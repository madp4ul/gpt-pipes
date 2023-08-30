namespace ChatBotPipes.WinformsApp.Controls;

using ChatBotPipes.Client.Implementation;
using ChatBotPipes.Core.Implementation;
using ChatBotPipes.Core.TaskTemplates;
using ChatBotPipes.WinformsApp.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class TaskTemplateEditor : UserControl
{
    private IChatBotProvider _chatBotProvider = null!;
    private ITaskTemplateFiller _taskTemplateFiller = null!;

    public event EventHandler<TaskTemplate>? TaskTemplateUpdated;

    public event EventHandler<TaskTemplate>? TaskTemplateDeleted;

    public TaskTemplate? TaskTemplate { get; private set; }

    private bool _canEdit = true;
    public bool CanEdit
    {
        get => _canEdit;
        set
        {
            _canEdit = value;
            SetControlsEditable();
        }
    }

    public TaskTemplateEditor()
    {
        InitializeComponent();
    }

    private void TaskTemplateEditor_Load(object sender, EventArgs e)
    {
        if (!DesignMode)
        {
            _chatBotProvider = Services.Get<IChatBotProvider>();
            _taskTemplateFiller = Services.Get<ITaskTemplateFiller>();

            SetControlsEditable();

            chatBotSelectionComboBox.Items.Clear();
            chatBotSelectionComboBox.Items.AddRange(_chatBotProvider.GetBotNames().Prepend("").ToArray());
        }
    }

    private void SetControlsEditable()
    {
        addMessageButton.Visible = CanEdit;
        deleteTaskTemplateButton.Visible = CanEdit;
        taskTemplateNameTextBox.Enabled = CanEdit;
        chatBotSelectionComboBox.Enabled = CanEdit;

        foreach (var chatMessageControl in chatMessagePanel.Rows.OfType<ChatMessageControl>())
        {
            chatMessageControl.CanEdit = CanEdit;
        }
    }

    public void SetTaskTemplateToEdit(TaskTemplate taskTemplate)
    {
        TaskTemplate = taskTemplate;

        taskTemplateNameTextBox.Text = taskTemplate.Name;

        UpdateComboBoxFromChatBotName(taskTemplate);
        UpdateInputListLabel(taskTemplate);

        foreach (var chatMessageControl in chatMessagePanel.Rows.OfType<ChatMessageControl>())
        {
            chatMessageControl.MessageUpdated -= ChatMessageControl_MessageUpdated;
            chatMessageControl.MessageDeleted -= ChatMessageControl_MessageDeleted;
        }

        chatMessagePanel.ClearRows();

        for (int i = 0; i < taskTemplate.PredefinedInstructions.Count; i++)
        {
            AddChatMessage(taskTemplate, i);
        }
    }

    private void UpdateComboBoxFromChatBotName(TaskTemplate taskTemplate)
    {
        for (int i = 0; i < chatBotSelectionComboBox.Items.Count; i++)
        {
            string chatBotName = (string)chatBotSelectionComboBox.Items[i];

            if (chatBotName == taskTemplate.ChatBotName)
            {
                chatBotSelectionComboBox.SelectedIndex = i;
                return;
            }
        }
        chatBotSelectionComboBox.SelectedIndex = 0;
    }

    private void AddChatMessage(TaskTemplate taskTemplate, int chatMessageIndex)
    {
        var chatMessageControl = new ChatMessageControl()
        {
            CanEdit = CanEdit
        };

        chatMessageControl.SetChatMessage(taskTemplate.PredefinedInstructions[chatMessageIndex]);

        chatMessageControl.MessageUpdated += ChatMessageControl_MessageUpdated;
        chatMessageControl.MessageDeleted += ChatMessageControl_MessageDeleted;

        chatMessagePanel.AddRow(chatMessageControl);
    }

    private void ChatMessageControl_MessageUpdated(object? sender, EventArgs e)
    {
        UpdateTaskTemplate(UpdateInputListLabel);
    }

    private void UpdateInputListLabel(TaskTemplate template)
    {
        var inputs = _taskTemplateFiller.GetInputs(template);

        inputListLabel.Text = string.Join(", ", inputs);
    }

    private void ChatMessageControl_MessageDeleted(object? sender, EventArgs e)
    {
        if (sender is not ChatMessageControl chatMessageControl)
        {
            throw new ApplicationException("Received message deleted from something else than chat message control.");
        }

        UpdateTaskTemplate(template =>
        {
            var chatMessage = chatMessageControl.ChatMessage
                ?? throw new ApplicationException("Found chat message control that did not have a chat message during removal");

            template.PredefinedInstructions.Remove(chatMessage);

            chatMessagePanel.RemoveRow(chatMessageControl);
        });
    }

    private void DeleteTaskTemplateButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplate);

        if (MessageBox.Show("Are you sure you want to delete this task?", "Confirm deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            TaskTemplateDeleted?.Invoke(this, TaskTemplate);
        }
    }

    private void UpdateTaskTemplate(Action<TaskTemplate> updateAction)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplate);

        updateAction(TaskTemplate);

        TaskTemplateUpdated?.Invoke(this, TaskTemplate);
    }

    private void TaskTemplateNameTextBox_Leave(object sender, EventArgs e)
    {
        UpdateTaskTemplate(t => t.Name = taskTemplateNameTextBox.Text);
    }

    private void AddMessageButton_Click(object sender, EventArgs e)
    {
        UpdateTaskTemplate(template =>
        {
            template.PredefinedInstructions.Add(new ChatMessage("...", ChatMessageAuthor.User));

            AddChatMessage(template, template.PredefinedInstructions.Count - 1);
        });
    }

    private void RunButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplate);

        var runnerForm = new TaskRunnerForm();

        runnerForm.SetTaskTemplate(TaskTemplate);

        runnerForm.ShowDialog();
    }

    private void ChatBotSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateTaskTemplate(template => template.ChatBotName = chatBotSelectionComboBox.SelectedItem.ToString());
    }
}
