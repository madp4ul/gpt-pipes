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

        var inputVariables = pipe.GetRequiredInputs(_taskTemplateFiller);

        foreach (PipeTaskTemplateVariableReference inputVariable in inputVariables)
        {
            var userInputControl = new PipeRunnerVariableUserInputControl();

            userInputControl.SetReferencedVariable(inputVariable);
            userInputControl.UserInputChanged += UserInputControl_UserInputChanged;

            userInputPanel.AddRow(userInputControl);
        }
    }

    private void UserInputControl_UserInputChanged(object? sender, PipeRunnerVariableUserInputControl.PipeInputChange inputChange)
    {
        ArgumentNullException.ThrowIfNull(_pipeVariableValueMap);

        _pipeVariableValueMap.SetInputValue(inputChange.InputName, inputChange.UserInputValue);
    }

    private void CreateTaskControls(Pipe pipe)
    {
        foreach (var taskTemplateUsage in pipe.Tasks)
        {
            var taskControl = new PipeRunnerTaskOutputControl();

            taskControl.SetTaskTemplate(taskTemplateUsage);

            taskControl.RegenerateOutputRequested += TaskControl_RegenerateOutputRequested;

            _pipeRunnerTaskControls.Add(taskTemplateUsage, taskControl);
            outputPanel.AddRow(taskControl);
        }
    }

    private async void TaskControl_RegenerateOutputRequested(object? sender, PipeTaskTemplateUsage taskUsage)
    {
        ArgumentNullException.ThrowIfNull(_pipeVariableValueMap);

        _pipeVariableValueMap.RemoveOutputValue(taskUsage);

        ClearMissingOutputControls();

        await FillMissingOutputsAsync();
    }

    private async void RunButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(_pipeVariableValueMap);

        _pipeVariableValueMap.ClearOutputValues();
        ClearMissingOutputControls();

        await FillMissingOutputsAsync();
    }

    private async Task FillMissingOutputsAsync()
    {
        _cancellationTokenSource?.Cancel();

        var cancellationTokenSource = new CancellationTokenSource();
        _cancellationTokenSource = cancellationTokenSource;

        SetAreControlsEnabled(false);

        try
        {
            await RunPipeAsync(_cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        { }
        finally
        {
            if (cancellationTokenSource == _cancellationTokenSource)
            {
                // Only set enabled if the cancellation is still our own.
                SetAreControlsEnabled(true);
            }
        }
    }

    private async Task RunPipeAsync(CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(Pipe);
        ArgumentNullException.ThrowIfNull(_pipeVariableValueMap);

        var result = _pipeRunner.RunPipeAsync(Pipe, _pipeVariableValueMap, _taskTemplateFiller, cancellationToken);

        int index = 0;
        await foreach (var pipeResponse in result.TaskResponses)
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

    private void ClearMissingOutputControls()
    {
        ArgumentNullException.ThrowIfNull(_pipeVariableValueMap);

        foreach (var control in _pipeRunnerTaskControls.Values)
        {
            var controlTaskUsage = control.TaskTemplateUsage ?? throw new ArgumentNullException(nameof(control.TaskTemplateUsage));

            bool isControlOutputDefined = _pipeVariableValueMap.Get(controlTaskUsage).HasOutput();

            if (!isControlOutputDefined)
            {
                control.Clear();
            }
        }
    }
}
