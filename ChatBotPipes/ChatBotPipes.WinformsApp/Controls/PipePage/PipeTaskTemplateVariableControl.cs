﻿namespace ChatBotPipes.WinformsApp.Controls;

using ChatBotPipes.Core.Pipes;
using ChatBotPipes.Core.TaskTemplates;
using ChatBotPipes.WinformsApp.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

public partial class PipeTaskTemplateVariableControl : UserControl
{
    private readonly ITaskTemplateFiller _templateFiller = null!;

    public event EventHandler<InputMappingChange>? InputMappingChanged;

    public PipeInputMappingControlData? Data { get; private set; }

    public PipeTaskTemplateVariableControl()
    {
        InitializeComponent();

        if (!DesignMode)
        {
            _templateFiller = Services.Get<ITaskTemplateFiller>();
        }
    }

    public void SetData(PipeInputMappingControlData data)
    {
        Data = data;

        inputNameLabel.Text = data.InputName + ":";
        variableReferenceLabel.Text = GetVariableReferenceDescription(data);
    }

    private static string GetVariableReferenceDescription(PipeInputMappingControlData data)
    {
        if (data.VariableReference is null)
        {
            return "user input";
        }

        string taskName = data.SourcePipe.GetTaskNameInContext(data.VariableReference.ReferencedTaskTemplate);

        return $"{data.VariableReference.ReferencedVariableName} from '{taskName}'";
    }

    private void UpdateVariableReferenceButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(Data);

        var referencibleTaskTemplates = Data.SourcePipe.Tasks
            .TakeWhile(tt => tt != Data.SourceTaskTemplate)
            .ToList();

        var referencibleVariables = referencibleTaskTemplates
            .ToDictionary(tt => tt, tt => tt.TaskTemplate.GetReferencibleVariableNames(_templateFiller).ToList());

        var selectVariableReferenceForm = new SelectVariableForInputForm();

        selectVariableReferenceForm.SetValidVariables(Data.SourcePipe, referencibleVariables);

        if (selectVariableReferenceForm.ShowDialog() == DialogResult.OK)
        {
            Data = Data with { VariableReference = selectVariableReferenceForm.SelectedVariable };

            InputMappingChanged?.Invoke(this, new InputMappingChange(Data.InputName, selectVariableReferenceForm.SelectedVariable));
        }
    }

    public record PipeInputMappingControlData(string InputName, PipeTaskTemplateVariableReference? VariableReference, PipeTaskTemplateUsage SourceTaskTemplate, Pipe SourcePipe);

    public record InputMappingChange(string InputName, PipeTaskTemplateVariableReference? VariableReference);
}
