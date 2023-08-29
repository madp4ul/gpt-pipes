namespace ChatBotPipes.WinformsApp.Controls;

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

public partial class ChatMessageControl : UserControl
{
    public event EventHandler? MessageUpdated;
    public event EventHandler? MessageDeleted;

    public ChatMessage? ChatMessage { get; private set; }

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

    public ChatMessageControl()
    {
        InitializeComponent();
    }

    public void SetChatMessage(ChatMessage chatMessage)
    {
        ChatMessage = chatMessage;

        authorComboBox.Items.AddRange(
            Enum.GetValues<ChatMessageAuthor>()
                .Select(e => e.ToString())
                .ToArray());

        authorComboBox.SelectedIndex = (int)chatMessage.Author;

        messageTextBox.Text = ChatMessage.Text;

        SetControlsEditable();
    }

    private void SetControlsEditable()
    {
        authorComboBox.Enabled = CanEdit;
        messageTextBox.Enabled = CanEdit;
        deleteButton.Visible = CanEdit;
    }

    private void AuthorComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(ChatMessage);

        ChatMessage.Author = (ChatMessageAuthor)authorComboBox.SelectedIndex;

        MessageUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void MessageTextBox_Leave(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(ChatMessage);

        ChatMessage.Text = messageTextBox.Text;

        MessageUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        MessageDeleted?.Invoke(this, EventArgs.Empty);
    }
}
