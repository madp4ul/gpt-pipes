namespace ChatBotPipes.WinformsApp.Controls;

using ChatBotPipes.Client.Implementation;
using ChatBotPipes.Core.Pipes;
using ChatBotPipes.Core.TaskTemplates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class PipeTaskTemplateControl : UserControl
{
    private readonly IChatBotProvider _chatBotProvider = null!;
    private readonly ITaskTemplateFiller _taskTemplateFiller = null!;

    public event EventHandler<PipeTaskTemplateUsage>? InsertAboveRequested;
    public event EventHandler<PipeTaskTemplateUsage>? MoveUpRequested;
    public event EventHandler<PipeTaskTemplateUsage>? MoveDownRequested;
    public event EventHandler<PipeTaskTemplateUsage>? RemoveRequested;
    public event EventHandler<PipeTaskTemplateUsage>? InputMappingUpdated;

    public PipeTaskTemplateUsage? TaskTemplateMapping { get; private set; }
    public Pipe? SourcePipe { get; private set; }

    public PipeTaskTemplateControl()
    {
        InitializeComponent();

        if (!DesignMode)
        {
            _chatBotProvider = Services.Get<IChatBotProvider>();
            _taskTemplateFiller = Services.Get<ITaskTemplateFiller>();
        }
    }

    public void SetTaskTemplate(PipeTaskTemplateUsage taskTemplateMapping, Pipe sourcePipe)
    {
        TaskTemplateMapping = taskTemplateMapping;
        SourcePipe = sourcePipe;

        nameLabel.Text = sourcePipe.GetTaskNameInContext(taskTemplateMapping);
        chatBotNameLabel.Text = !string.IsNullOrEmpty(taskTemplateMapping.TaskTemplate.ChatBotName)
            ? taskTemplateMapping.TaskTemplate.ChatBotName
            : $"{_chatBotProvider.GetDefaultBotName()} (default)";

        UpdateInputMappingControls(taskTemplateMapping, sourcePipe);
    }

    private void UpdateInputMappingControls(PipeTaskTemplateUsage taskTemplateMapping, Pipe sourcePipe)
    {
        foreach (var control in inputMappingPanel.Rows.OfType<PipeTaskTemplateVariableControl>())
        {
            control.InputMappingChanged -= Control_InputMappingChanged;
        }

        inputMappingPanel.ClearRows();

        var inputs = _taskTemplateFiller.GetInputs(taskTemplateMapping.TaskTemplate);

        foreach (var input in inputs)
        {
            var control = new PipeTaskTemplateVariableControl();

            var variableReference = taskTemplateMapping.InputMapping.GetValueOrDefault(input);

            var data = new PipeTaskTemplateVariableControl.PipeInputMappingControlData(input, variableReference, taskTemplateMapping, sourcePipe);

            control.SetData(data);

            control.InputMappingChanged += Control_InputMappingChanged;

            inputMappingPanel.AddRow(control);
        }
    }

    private void Control_InputMappingChanged(object? sender, PipeTaskTemplateVariableControl.InputMappingChange mapping)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplateMapping);
        ArgumentNullException.ThrowIfNull(SourcePipe);

        if (mapping.VariableReference is null)
        {
            TaskTemplateMapping.InputMapping.Remove(mapping.InputName);
        }
        else
        {
            TaskTemplateMapping.InputMapping[mapping.InputName] = mapping.VariableReference;
        }

        UpdateInputMappingControls(TaskTemplateMapping, SourcePipe);

        InputMappingUpdated?.Invoke(this, TaskTemplateMapping);
    }

    private void InsertAboveButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplateMapping);

        InsertAboveRequested?.Invoke(this, TaskTemplateMapping);
    }

    private void MoveUpButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplateMapping);

        MoveUpRequested?.Invoke(this, TaskTemplateMapping);
    }

    private void MoveDownButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplateMapping);

        MoveDownRequested?.Invoke(this, TaskTemplateMapping);
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplateMapping);

        RemoveRequested?.Invoke(this, TaskTemplateMapping);
    }
}
