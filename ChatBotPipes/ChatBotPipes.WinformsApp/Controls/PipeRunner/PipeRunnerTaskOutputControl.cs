namespace ChatBotPipes.WinformsApp.Controls;

using ChatBotPipes.Client;
using ChatBotPipes.Core.TaskTemplates;
using ChatBotPipes.WinformsApp.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

public partial class PipeRunnerTaskOutputControl : UserControl
{
    private StringBuilder _outputStringBuilder = new();

    public TaskTemplate? TaskTemplate { get; private set; }

    public PipeRunnerTaskOutputControl()
    {
        InitializeComponent();
    }

    public void SetTaskTemplate(TaskTemplate taskTemplate)
    {
        TaskTemplate = taskTemplate;

        taskTemplateNameLabel.Text = taskTemplate.Name;
    }

    public async Task UpdateFromChatbotResponseAsync(IChatBotResponse chatBotResponse)
    {
        Clear();

        outputTextBox.Text = chatBotResponse.GetCurrentResponse();

        chatBotResponse.DataReceived += ChatBotResponse_DataReceived;
        updateOutputTimer.Start();

        try
        {
            outputTextBox.Text = await chatBotResponse.AwaitCompletionAsync();
        }
        catch (OperationCanceledException)
        { }
        finally
        {
            updateOutputTimer.Stop();
            chatBotResponse.DataReceived -= ChatBotResponse_DataReceived;
        }
    }

    public void Clear()
    {
        _outputStringBuilder.Clear();
        outputTextBox.Text = "";
    }

    private void ChatBotResponse_DataReceived(string additonalText)
    {
        _outputStringBuilder.Append(additonalText);
    }

    private void RunButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplate);

        var runnerForm = new TaskRunnerForm();

        runnerForm.SetTaskTemplate(TaskTemplate);

        runnerForm.ShowDialog();
    }

    [GeneratedRegex("\n")]
    private static partial Regex NewLineRegex();

    private void UpdateOutputTimer_Tick(object sender, EventArgs e)
    {
        outputTextBox.Text = _outputStringBuilder.ToString();
    }
}