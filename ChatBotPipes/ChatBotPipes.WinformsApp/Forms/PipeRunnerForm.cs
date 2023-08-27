﻿namespace ChatBotPipes.WinformsApp.Forms;
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
using System.Xml;

public partial class PipeRunnerForm : Form
{
    private readonly IChatBotPipeRunner _pipeRunner = null!;
    private readonly ITaskTemplateFiller _taskTemplateFiller = null!;

    public ChatBotPipe? Pipe { get; private set; }

    private PipeVariableValueMap? _pipeVariableValueMap;

    private readonly List<PipeRunnerTaskControl> _pipeRunnerTaskControls = new();

    public PipeRunnerForm()
    {
        InitializeComponent();

        if (!DesignMode)
        {
            _pipeRunner = Services.Get<IChatBotPipeRunner>();
            _taskTemplateFiller = Services.Get<ITaskTemplateFiller>();
        }
    }

    public void SetPipe(ChatBotPipe pipe)
    {
        Pipe = pipe;
        _pipeVariableValueMap = new PipeVariableValueMap();

        this.Text = $"Run pipe \"{pipe.Name}\"";
        pipeNameLabel.Text = pipe.Name;

        UpdateUserInputControls(pipe);
        CreateTaskControls(pipe);
    }

    private void UpdateUserInputControls(ChatBotPipe pipe)
    {
        foreach (var control in userInputPanel.Controls.OfType<UserPipeInputControl>())
        {
            control.UserInputChanged -= UserInputControl_UserInputChanged;
        }

        userInputPanel.Controls.Clear();

        var inputs = pipe.GetRequiredInputs(_taskTemplateFiller);

        foreach (TaskTemplateVariableName input in inputs)
        {
            var userInputControl = new UserPipeInputControl()
            {
                Width = userInputPanel.Width - 30
            };

            userInputControl.SetUserInputName(input);
            userInputControl.UserInputChanged += UserInputControl_UserInputChanged;

            userInputPanel.Controls.Add(userInputControl);
        }
    }

    private void UserInputControl_UserInputChanged(object? sender, UserPipeInputControl.PipeInputChange inputChange)
    {
        ArgumentNullException.ThrowIfNull(_pipeVariableValueMap);

        _pipeVariableValueMap.AddInputValue(inputChange.InputName, inputChange.UserInputValue);
    }

    private void CreateTaskControls(ChatBotPipe pipe)
    {
        foreach (var taskMapping in pipe.Tasks)
        {
            var taskControl = new PipeRunnerTaskControl
            {
                Width = outputPanel.Width - 30
            };

            taskControl.SetTaskTemplate(taskMapping.TaskTemplate);

            _pipeRunnerTaskControls.Add(taskControl);
            outputPanel.Controls.Add(taskControl);
        }
    }

    private async void RunButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(Pipe);
        ArgumentNullException.ThrowIfNull(_pipeVariableValueMap);

        var responseEnumerable = _pipeRunner.RunPipeAsync(Pipe, _pipeVariableValueMap, _taskTemplateFiller);

        int index = 0;
        await foreach (var response in responseEnumerable)
        {
            var control = _pipeRunnerTaskControls[index];

            await control.UpdateFromChatbotResponseAsync(response);

            index++;
        }
    }
}
