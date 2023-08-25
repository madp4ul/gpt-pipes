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
    private IChatBotPipeRunner _pipeRunner = null!;
    private ITaskTemplateFiller _taskTemplateFiller = null!;

    public ChatBotPipe? Pipe { get; private set; }

    private List<PipeRunnerTaskControl> _pipeRunnerTaskControls = new List<PipeRunnerTaskControl>();

    public PipeRunnerForm()
    {
        InitializeComponent();
    }

    private void PipeRunnerForm_Load(object sender, EventArgs e)
    {
        if (DesignMode)
        {
            return;
        }

        _pipeRunner = Services.Get<IChatBotPipeRunner>();
        _taskTemplateFiller = Services.Get<ITaskTemplateFiller>();
    }

    public void SetPipe(ChatBotPipe pipe)
    {
        Pipe = pipe;

        this.Text = $"Run pipe \"{pipe.Name}\"";
        pipeNameLabel.Text = pipe.Name;

        CreateTaskControls(pipe);
    }

    private void CreateTaskControls(ChatBotPipe pipe)
    {
        foreach (var task in pipe.Tasks)
        {
            var taskControl = new PipeRunnerTaskControl
            {
                Width = outputPanel.Width - 30
            };

            taskControl.SetTaskTemplate(task);

            _pipeRunnerTaskControls.Add(taskControl);
            outputPanel.Controls.Add(taskControl);
        }
    }

    private async void RunButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(Pipe);

        var responseEnumerable = _pipeRunner.RunPipeAsync(Pipe, inputTextBox.Text, _taskTemplateFiller);

        int index = 0;
        await foreach (var response in responseEnumerable)
        {
            var control = _pipeRunnerTaskControls[index];

            await control.UpdateFromChatbotResponseAsync(response);

            index++;
        }
    }
}
