namespace ChatBotPipes.WinformsApp.Controls;

using ChatBotPipes.Client;
using ChatBotPipes.Core;
using ChatBotPipes.WinformsApp.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

public partial class PipeRunnerTaskControl : UserControl
{
    public ChatBotTaskTemplate? TaskTemplate { get; private set; }

    public PipeRunnerTaskControl()
    {
        InitializeComponent();
    }

    public void SetTaskTemplate(ChatBotTaskTemplate taskTemplate)
    {
        TaskTemplate = taskTemplate;

        taskTemplateNameLabel.Text = taskTemplate.Name;
    }

    public async Task UpdateFromChatbotResponseAsync(IChatBotResponse chatBotResponse)
    {
        outputTextBox.OutputText = chatBotResponse.GetCurrentResponse();

        chatBotResponse.DataReceived += ChatBotResponse_DataReceived;

        outputTextBox.OutputText = await chatBotResponse.AwaitCompletionAsync();

        chatBotResponse.DataReceived -= ChatBotResponse_DataReceived;
    }

    private void ChatBotResponse_DataReceived(string additonalText)
    {
        outputTextBox.OutputText += additonalText;
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
}