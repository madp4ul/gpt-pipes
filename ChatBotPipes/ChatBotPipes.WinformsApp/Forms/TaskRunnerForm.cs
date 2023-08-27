namespace ChatBotPipes.WinformsApp.Forms;

using ChatBotPipes.Client;
using ChatBotPipes.Core;
using ChatBotPipes.WinformsApp.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class TaskRunnerForm : Form
{
    private readonly IChatBotTaskRunner _taskRunner = null!;
    private readonly ITaskTemplateFiller _taskTemplateFiller = null!;

    public ChatBotTaskTemplate? TaskTemplate { get; private set; }

    private TaskVariableValueMap? _taskVariableValueMap;

    public TaskRunnerForm()
    {
        InitializeComponent();

        outputTextBox.OutputText = "";

        if (!DesignMode)
        {
            _taskRunner = Services.Get<IChatBotTaskRunner>();
            _taskTemplateFiller = Services.Get<ITaskTemplateFiller>();
        }
    }

    public void SetTaskTemplate(ChatBotTaskTemplate taskTemplate)
    {
        TaskTemplate = taskTemplate;
        _taskVariableValueMap = new TaskVariableValueMap();

        nameLabel.Text = taskTemplate.Name;
        Text = $"Run Task \"{taskTemplate.Name}\"";

        UpdateUserInputControls(taskTemplate);
    }

    private void UpdateUserInputControls(ChatBotTaskTemplate taskTemplate)
    {
        foreach (var control in userInputPanel.Controls.OfType<UserTaskInputControl>())
        {
            control.UserInputChanged -= UserInputControl_UserInputChanged;
        }

        userInputPanel.Controls.Clear();

        var inputs = _taskTemplateFiller.GetInputs(taskTemplate);

        foreach (string input in inputs)
        {
            var userInputControl = new UserTaskInputControl()
            {
                Width = userInputPanel.Width - 30
            };

            userInputControl.SetUserInputName(input);
            userInputControl.UserInputChanged += UserInputControl_UserInputChanged;

            userInputPanel.Controls.Add(userInputControl);
        }
    }

    private void UserInputControl_UserInputChanged(object? sender, UserTaskInputControl.InputChange inputChange)
    {
        ArgumentNullException.ThrowIfNull(_taskVariableValueMap);

        _taskVariableValueMap.AddInputValue(inputChange.InputName, inputChange.UserInputValue);

        // todo update chat preview
    }

    private async void RunButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplate);
        ArgumentNullException.ThrowIfNull(_taskVariableValueMap);

        SetAreControlsEnabled(false);
        outputTextBox.OutputText = "";

        var response = await _taskRunner.RunTaskAsync(TaskTemplate, _taskVariableValueMap, _taskTemplateFiller);

        response.DataReceived += Response_DataReceived;

        await response.AwaitCompletionAsync();

        response.DataReceived -= Response_DataReceived;

        SetAreControlsEnabled(true);
    }

    private void SetAreControlsEnabled(bool enabled)
    {
        runButton.Enabled = enabled;

        foreach (var control in userInputPanel.Controls.OfType<UserTaskInputControl>())
        {
            control.Enabled = enabled;
        }
    }

    private void Response_DataReceived(string additionalText)
    {
        outputTextBox.OutputText += additionalText;
    }
}
