namespace ChatBotPipes.WinformsApp.Controls;

using ChatBotPipes.Client.Implementation;
using ChatBotPipes.Core;
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
    private readonly IChatBotProvider _chatBotProvider = null!;

    public event EventHandler<ChatBotTaskTemplate>? TaskTemplateUpdated;

    public event EventHandler<ChatBotTaskTemplate>? TaskTemplateDeleted;

    public ChatBotTaskTemplate? TaskTemplate { get; private set; }

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

        SetControlsEditable();

        if (!DesignMode)
        {
            _chatBotProvider = Services.Get<IChatBotProvider>();

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

        foreach (var chatMessageControl in chatMessagePanel.Controls.OfType<ChatMessageControl>())
        {
            chatMessageControl.CanEdit = CanEdit;
        }
    }

    public void SetTaskTemplateToEdit(ChatBotTaskTemplate taskTemplate)
    {
        TaskTemplate = taskTemplate;

        taskTemplateNameTextBox.Text = taskTemplate.Name;

        UpdateComboBoxFromChatBotName(taskTemplate);

        foreach (var chatMessageControl in chatMessagePanel.Controls.OfType<ChatMessageControl>())
        {
            chatMessageControl.MessageUpdated -= ChatMessageControl_MessageUpdated;
            chatMessageControl.MessageDeleted -= ChatMessageControl_MessageDeleted;
        }

        chatMessagePanel.Controls.Clear();

        for (int i = 0; i < taskTemplate.PredefinedInstructions.Count; i++)
        {
            AddChatMessage(taskTemplate, i);
        }
    }

    private void UpdateComboBoxFromChatBotName(ChatBotTaskTemplate taskTemplate)
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

    private void AddChatMessage(ChatBotTaskTemplate taskTemplate, int chatMessageIndex)
    {
        var chatMessageControl = new ChatMessageControl(taskTemplate.PredefinedInstructions[chatMessageIndex])
        {
            Width = chatMessagePanel.Width - 20,
            Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
            CanEdit = CanEdit
        };

        chatMessageControl.MessageUpdated += ChatMessageControl_MessageUpdated;
        chatMessageControl.MessageDeleted += ChatMessageControl_MessageDeleted;

        chatMessageControl.Top = chatMessageIndex * chatMessageControl.Height + chatMessagePanel.AutoScrollPosition.Y;

        chatMessagePanel.Controls.Add(chatMessageControl);
    }

    private void ChatMessageControl_MessageUpdated(object? sender, EventArgs e)
    {
        UpdateTaskTemplate(_ =>
        {
            // chat message is already modifed, nothing else to do
        });
    }

    private void ChatMessageControl_MessageDeleted(object? sender, EventArgs e)
    {
        if (sender is not ChatMessageControl chatMessageControl)
        {
            throw new ApplicationException("Received message deleted from something else than chat message control.");
        }

        UpdateTaskTemplate(template =>
        {
            template.PredefinedInstructions.Remove(chatMessageControl.ChatMessage);

            chatMessagePanel.Controls.Remove(chatMessageControl);
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

    private void UpdateTaskTemplate(Action<ChatBotTaskTemplate> updateAction)
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
