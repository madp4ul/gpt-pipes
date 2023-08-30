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

    private CancellationTokenSource? _cancellationTokenSource;

    public ChatBotTaskTemplate? TaskTemplate { get; private set; }

    private TaskVariableValueMap? _taskVariableValueMap;

    public TaskRunnerForm()
    {
        InitializeComponent();

        outputTextBox.Text = "";

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
        foreach (var control in userInputPanel.Rows.OfType<UserTaskInputControl>())
        {
            control.UserInputChanged -= UserInputControl_UserInputChanged;
        }

        userInputPanel.ClearRows();

        var inputs = _taskTemplateFiller.GetInputs(taskTemplate);

        foreach (string input in inputs)
        {
            var userInputControl = new UserTaskInputControl();

            userInputControl.SetUserInputName(input);
            userInputControl.UserInputChanged += UserInputControl_UserInputChanged;

            userInputPanel.AddRow(userInputControl);
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
        SetAreControlsEnabled(false);

        _cancellationTokenSource = new CancellationTokenSource();

        try
        {
            await RunTaskAsync(_cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        { }
        finally
        {
            SetAreControlsEnabled(true);
        }
    }

    private async Task RunTaskAsync(CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplate);
        ArgumentNullException.ThrowIfNull(_taskVariableValueMap);

        outputTextBox.Text = "";

        var response = await _taskRunner.RunTaskAsync(TaskTemplate, _taskVariableValueMap, _taskTemplateFiller, cancellationToken);

        response.DataReceived += Response_DataReceived;

        await response.AwaitCompletionAsync();

        response.DataReceived -= Response_DataReceived;
    }

    private void SetAreControlsEnabled(bool enabled)
    {
        runButton.Enabled = enabled;
        cancelButton.Enabled = !enabled;

        foreach (var control in userInputPanel.Rows.OfType<UserTaskInputControl>())
        {
            control.Enabled = enabled;
        }
    }

    private void Response_DataReceived(string additionalText)
    {
        outputTextBox.Text += additionalText;
    }

    private void ButtonCancel_Click(object sender, EventArgs e)
    {
        _cancellationTokenSource?.Cancel();
    }
}
