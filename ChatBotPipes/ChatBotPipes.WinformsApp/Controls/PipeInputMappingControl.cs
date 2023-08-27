namespace ChatBotPipes.WinformsApp.Controls;

using ChatBotPipes.Core;
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

public partial class PipeInputMappingControl : UserControl
{
    private readonly ITaskTemplateFiller _templateFiller = null!;

    public event EventHandler<InputMappingChange>? InputMappingChanged;

    public PipeInputMappingControlData? Data { get; private set; }

    public PipeInputMappingControl()
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
        variableReferenceLabel.Text = GetVariableReferenceDescription(data.VariableReference);

    }

    private static string GetVariableReferenceDescription(TaskTemplateVariableName? variableReference)
    {
        if (variableReference is null)
        {
            return "user input";
        }

        return $"{variableReference.InputName} from '{variableReference.TaskTemplate.Name}'";
    }

    private void UpdateVariableReferenceButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(Data);

        var referencibleTaskTemplates = Data.SourcePipe.Tasks
            .TakeWhile(tt => tt.TaskTemplate != Data.SourceTaskTemplate.TaskTemplate)
            .Select(tt => tt.TaskTemplate)
            .ToList();

        var referencibleVariables = referencibleTaskTemplates
            .ToDictionary(tt => tt, tt => tt.GetReferencibleVariableNames(_templateFiller).ToList());

        var selectVariableReferenceForm = new SelectVariableForInputForm();

        selectVariableReferenceForm.SetValidVariables(referencibleVariables);

        if (selectVariableReferenceForm.ShowDialog() == DialogResult.OK)
        {
            Data = Data with { VariableReference = selectVariableReferenceForm.SelectedVariable };

            InputMappingChanged?.Invoke(this, new InputMappingChange(Data.InputName, selectVariableReferenceForm.SelectedVariable));
        }
    }

    public record PipeInputMappingControlData(string InputName, TaskTemplateVariableName? VariableReference, MappedChatBotTaskTemplate SourceTaskTemplate, ChatBotPipe SourcePipe);

    public record InputMappingChange(string InputName, TaskTemplateVariableName? VariableReference);
}
