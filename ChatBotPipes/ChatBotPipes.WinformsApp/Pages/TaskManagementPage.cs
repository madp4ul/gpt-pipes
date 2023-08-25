﻿namespace ChatBotPipes.WinformsApp.Pages;

using ChatBotPipes.Core;
using ChatBotPipes.Server;
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

public partial class TaskManagementPage : UserControl
{
    private IChatBotTaskTemplateStore _taskTemplateStore = null!;

    private List<ChatBotTaskTemplate> _taskTemplates = new();

    public event EventHandler<ChatBotTaskTemplate?>? SelectedTaskTemplateChanged;

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

    public TaskManagementPage()
    {
        InitializeComponent();

        taskTemplateListBox.DisplayMember = nameof(ChatBotTaskTemplate.Name);

        SetControlsEditable();
    }

    private void InitializeServices()
    {
        _taskTemplateStore = Services.Get<IChatBotTaskTemplateStore>();
    }

    private async void TaskManagement_Load(object sender, EventArgs e)
    {
        if (DesignMode)
        {
            return;
        }

        InitializeServices();

        _taskTemplates = await _taskTemplateStore.GetTaskTemplatesAsync(AppUser.Default);

        if (_taskTemplates.Count == 0)
        {
            await AddTaskTemplateAsync();
        }

        UpdateTaskTemplateList();
    }

    private void SetControlsEditable()
    {
        addTaskTemplateButton.Visible = CanEdit;
        taskTemplateEditor.CanEdit = CanEdit;
    }

    private async void AddTaskTemplateButton_Click(object sender, EventArgs e)
    {
        await AddTaskTemplateAsync();
    }

    private async Task AddTaskTemplateAsync()
    {
        var newTaskTemplate = new ChatBotTaskTemplate(
            new List<ChatMessage> { new ChatMessage("System message", ChatMessageAuthor.System) },
            "New Task");

        await _taskTemplateStore.AddTaskTemplateAsync(AppUser.Default, newTaskTemplate);

        _taskTemplates.Add(newTaskTemplate);

        UpdateTaskTemplateList();

        taskTemplateListBox.SelectedIndex = taskTemplateListBox.Items.Count - 1;
    }

    private void UpdateTaskTemplateList()
    {
        taskTemplateListBox.Items.Clear();
        taskTemplateListBox.Items.AddRange(_taskTemplates.ToArray());
    }

    private void TaskTemplateListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (taskTemplateListBox.SelectedItem is ChatBotTaskTemplate selectedTemplate)
        {
            taskTemplateEditor.SetTaskTemplateToEdit(selectedTemplate);

            taskTemplateEditor.Visible = true;

            SelectedTaskTemplateChanged?.Invoke(this, selectedTemplate);
        }
        else
        {
            taskTemplateEditor.Visible = false;

            SelectedTaskTemplateChanged?.Invoke(this, null);
        }
    }

    private async void TaskTemplateEditor_TaskTemplateUpdated(object sender, ChatBotTaskTemplate e)
    {
        await _taskTemplateStore.UpdateTaskTemplateAsync(AppUser.Default, e);

        UpdateTaskTemplateList();
    }

    private async void TaskTemplateEditor_TaskTemplateDeleted(object sender, ChatBotTaskTemplate template)
    {
        await _taskTemplateStore.RemoveTaskTemplateAsync(AppUser.Default, template);

        _taskTemplates.Remove(template);

        taskTemplateEditor.Visible = false;

        UpdateTaskTemplateList();
    }
}
