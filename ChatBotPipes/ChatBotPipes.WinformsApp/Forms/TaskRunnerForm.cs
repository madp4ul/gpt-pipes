namespace ChatBotPipes.WinformsApp.Forms;

using ChatBotPipes.Client;
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

public partial class TaskRunnerForm : Form
{
    private IChatBotTaskRunner _taskRunner = null!;
    private ITaskTemplateFiller _taskTemplateFiller = null!;

    public ChatBotTaskTemplate? TaskTemplate { get; private set; }

    public TaskRunnerForm()
    {
        InitializeComponent();

        outputTextBox.OutputText = "";
    }

    private void TaskRunnerForm_Load(object sender, EventArgs e)
    {
        if (DesignMode)
        {
            return;
        }

        _taskRunner = Services.Get<IChatBotTaskRunner>();
        _taskTemplateFiller = Services.Get<ITaskTemplateFiller>();
    }

    public void SetTaskTemplate(ChatBotTaskTemplate taskTemplate)
    {
        TaskTemplate = taskTemplate;

        nameLabel.Text = taskTemplate.Name;
        Text = $"Run Task \"{taskTemplate.Name}\"";
    }

    private async void RunButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplate);

        runButton.Enabled = false;
        inputTextBox.Enabled = false;
        outputTextBox.OutputText = "";

        var response = await _taskRunner.RunTaskAsync(TaskTemplate, inputTextBox.Text, _taskTemplateFiller);

        response.DataReceived += Response_DataReceived;

        await response.AwaitCompletionAsync();

        response.DataReceived -= Response_DataReceived;

        runButton.Enabled = true;
        inputTextBox.Enabled = true;
    }

    private void Response_DataReceived(string additionalText)
    {
        outputTextBox.OutputText += additionalText;
    }
}
