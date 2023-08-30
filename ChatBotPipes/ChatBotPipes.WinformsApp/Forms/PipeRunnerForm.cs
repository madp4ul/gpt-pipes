namespace ChatBotPipes.WinformsApp.Forms;
using ChatBotPipes.Client;
using ChatBotPipes.Core.Pipes;
using ChatBotPipes.Core.TaskTemplates;
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
using System.Xml;

public partial class PipeRunnerForm : Form
{
    private readonly IChatBotPipeRunner _pipeRunner = null!;
    private readonly ITaskTemplateFiller _taskTemplateFiller = null!;
    private CancellationTokenSource? _cancellationTokenSource;

    public Pipe? Pipe { get; private set; }

    private PipeTemplateValues? _pipeVariableValueMap;

    private readonly Dictionary<PipeTaskTemplateUsage, PipeRunnerTaskOutputControl> _pipeRunnerTaskControls = new();

    public PipeRunnerForm()
    {
        InitializeComponent();

        if (!DesignMode)
        {
            _pipeRunner = Services.Get<IChatBotPipeRunner>();
            _taskTemplateFiller = Services.Get<ITaskTemplateFiller>();
        }
    }

    public void SetPipe(Pipe pipe)
    {
        Pipe = pipe;
        _pipeVariableValueMap = new PipeTemplateValues();

        this.Text = $"Run pipe \"{pipe.Name}\"";
        pipeNameLabel.Text = pipe.Name;

        UpdateUserInputControls(pipe);
        CreateTaskControls(pipe);
    }

    private void UpdateUserInputControls(Pipe pipe)
    {
        foreach (var control in userInputPanel.Rows.OfType<PipeRunnerVariableUserInputControl>())
        {
            control.UserInputChanged -= UserInputControl_UserInputChanged;
        }

        userInputPanel.ClearRows();

        var inputs = pipe.GetRequiredInputs(_taskTemplateFiller);

        foreach (PipeTaskTemplateVariableReference input in inputs)
        {
            var userInputControl = new PipeRunnerVariableUserInputControl();

            userInputControl.SetUserInputName(input);
            userInputControl.UserInputChanged += UserInputControl_UserInputChanged;

            userInputPanel.AddRow(userInputControl);
        }
    }

    private void UserInputControl_UserInputChanged(object? sender, PipeRunnerVariableUserInputControl.PipeInputChange inputChange)
    {
        ArgumentNullException.ThrowIfNull(_pipeVariableValueMap);

        _pipeVariableValueMap.AddInputValue(inputChange.InputName, inputChange.UserInputValue);
    }

    private void CreateTaskControls(Pipe pipe)
    {
        foreach (var taskMapping in pipe.Tasks)
        {
            var taskControl = new PipeRunnerTaskOutputControl();

            taskControl.SetTaskTemplate(taskMapping.TaskTemplate);

            _pipeRunnerTaskControls.Add(taskMapping, taskControl);
            outputPanel.AddRow(taskControl);
        }
    }

    private async void RunButton_Click(object sender, EventArgs e)
    {
        SetAreControlsEnabled(false);

        foreach (var control in _pipeRunnerTaskControls.Values)
        {
            control.Clear();
        }

        _cancellationTokenSource = new CancellationTokenSource();

        try
        {
            await RunPipeAsync(_cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        { }
        finally
        {
            SetAreControlsEnabled(true);
        }
    }

    private async Task RunPipeAsync(CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(Pipe);
        ArgumentNullException.ThrowIfNull(_pipeVariableValueMap);

        var responseEnumerable = _pipeRunner.RunPipeAsync(Pipe, _pipeVariableValueMap, _taskTemplateFiller, cancellationToken);

        int index = 0;
        await foreach (var pipeResponse in responseEnumerable)
        {
            var control = _pipeRunnerTaskControls[pipeResponse.Task];

            await control.UpdateFromChatbotResponseAsync(pipeResponse.Response);

            index++;
        }
    }

    private void SetAreControlsEnabled(bool enabled)
    {
        runButton.Enabled = enabled;
        cancelButton.Enabled = !enabled;

        foreach (var control in userInputPanel.Rows.OfType<PipeRunnerVariableUserInputControl>())
        {
            control.Enabled = enabled;
        }
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        _cancellationTokenSource?.Cancel();
    }
}
