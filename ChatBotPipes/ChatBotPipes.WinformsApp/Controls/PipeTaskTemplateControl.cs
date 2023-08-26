namespace ChatBotPipes.WinformsApp.Controls;

using ChatBotPipes.Client.Implementation;
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

public partial class PipeTaskTemplateControl : UserControl
{
    private readonly IChatBotProvider _chatBotProvider = null!;

    public event EventHandler<ChatBotTaskTemplate>? InsertAboveRequested;
    public event EventHandler<ChatBotTaskTemplate>? MoveUpRequested;
    public event EventHandler<ChatBotTaskTemplate>? MoveDownRequested;
    public event EventHandler<ChatBotTaskTemplate>? RemoveRequested;

    public ChatBotTaskTemplate? TaskTemplate { get; private set; }

    public PipeTaskTemplateControl()
    {
        InitializeComponent();

        if (!DesignMode)
        {
            _chatBotProvider = Services.Get<IChatBotProvider>();
        }
    }

    public void SetTaskTemplate(ChatBotTaskTemplate taskTemplate)
    {
        TaskTemplate = taskTemplate;

        nameLabel.Text = taskTemplate.Name;
        chatBotNameLabel.Text = taskTemplate.ChatBotName ?? $"{_chatBotProvider.GetDefaultBotName()} (default)";
    }

    private void InsertAboveButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplate);

        InsertAboveRequested?.Invoke(this, TaskTemplate);
    }

    private void MoveUpButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplate);

        MoveUpRequested?.Invoke(this, TaskTemplate);
    }

    private void MoveDownButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplate);

        MoveDownRequested?.Invoke(this, TaskTemplate);
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(TaskTemplate);

        RemoveRequested?.Invoke(this, TaskTemplate);
    }
}
