namespace ChatBotPipes.WinformsApp.Controls;

using ChatBotPipes.Client;
using ChatBotPipes.Core.Pipes;
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

    public event EventHandler<PipeTaskTemplateUsage>? RegenerateOutputRequested;

    public PipeTaskTemplateUsage? TaskTemplateUsage { get; private set; }

    public PipeRunnerTaskOutputControl()
    {
        InitializeComponent();
    }

    public void SetTaskTemplate(PipeTaskTemplateUsage taskTemplateUsage)
    {
        TaskTemplateUsage = taskTemplateUsage;

        taskTemplateNameLabel.Text = taskTemplateUsage.TaskTemplate.Name;
    }

    public async Task UpdateFromChatbotResponseAsync(IChatBotResponse chatBotResponse)
    {
        Clear();

        SetOutputText(chatBotResponse.GetCurrentResponse());

        chatBotResponse.DataReceived += ChatBotResponse_DataReceived;
        updateOutputTimer.Start();

        try
        {
            SetOutputText(await chatBotResponse.AwaitCompletionAsync());
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
        SetOutputText("");
    }

    private void ChatBotResponse_DataReceived(string additonalText)
    {
        _outputStringBuilder.Append(additonalText);
    }

    private void RunButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplateUsage);

        var runnerForm = new TaskRunnerForm();

        runnerForm.SetTaskTemplate(TaskTemplateUsage.TaskTemplate);

        runnerForm.ShowDialog();
    }

    [GeneratedRegex("\n")]
    private static partial Regex NewLineRegex();

    private void UpdateOutputTimer_Tick(object sender, EventArgs e)
    {
        SetOutputText(_outputStringBuilder.ToString());
    }

    private void SetOutputText(string text)
    {
        outputTextBox.Text = text;

        regenerateButton.Enabled = !string.IsNullOrEmpty(text);
    }

    private void RegenerateOutputButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplateUsage);

        RegenerateOutputRequested?.Invoke(this, TaskTemplateUsage);
    }
}