namespace ChatBotPipes.WinformsApp.Controls;

using ChatBotPipes.Core.Pipes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChatBotPipes.WinformsApp.Controls.UserTaskInputControl;

public partial class PipeRunnerVariableUserInputControl : UserControl
{
    public event EventHandler<PipeInputChange>? UserInputChanged;

    public PipeTaskTemplateVariableReference? ReferencedVariable { get; private set; }

    public PipeRunnerVariableUserInputControl()
    {
        InitializeComponent();
    }

    public void SetReferencedVariable(PipeTaskTemplateVariableReference taskVariableReference)
    {
        ReferencedVariable = taskVariableReference;

        userTaskInputControl.SetUserInputName($"{taskVariableReference.ReferencedVariableName} for '{taskVariableReference.ReferencedTaskTemplate.TaskTemplate.Name}':");
    }

    private void UserTaskInputControl_UserInputChanged(object sender, UserTaskInputControl.InputChange e)
    {
        ArgumentNullException.ThrowIfNull(ReferencedVariable);

        UserInputChanged?.Invoke(this, new PipeInputChange(ReferencedVariable, e.UserInputValue));
    }

    public record PipeInputChange(PipeTaskTemplateVariableReference InputName, string UserInputValue);
}
