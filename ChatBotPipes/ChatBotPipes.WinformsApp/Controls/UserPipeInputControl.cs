namespace ChatBotPipes.WinformsApp.Controls;

using ChatBotPipes.Core;
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

public partial class UserPipeInputControl : UserControl
{
    public event EventHandler<PipeInputChange>? UserInputChanged;

    public TaskTemplateVariableName? UserInputName { get; private set; }

    public UserPipeInputControl()
    {
        InitializeComponent();
    }

    public void SetUserInputName(TaskTemplateVariableName taskVariableName)
    {
        UserInputName = taskVariableName;

        userTaskInputControl.SetUserInputName($"{taskVariableName.InputName} for '{taskVariableName.TaskTemplate.TaskTemplate.Name}':");
    }

    private void UserTaskInputControl_UserInputChanged(object sender, UserTaskInputControl.InputChange e)
    {
        ArgumentNullException.ThrowIfNull(UserInputName);

        UserInputChanged?.Invoke(this, new PipeInputChange(UserInputName, e.UserInputValue));
    }

    public record PipeInputChange(TaskTemplateVariableName InputName, string UserInputValue);
}
