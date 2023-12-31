﻿namespace ChatBotPipes.WinformsApp.Pages;

using ChatBotPipes.Client;
using ChatBotPipes.Core.Pipes;
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

public partial class PipeManagementPage : UserControl
{
    private IPipeStore _chatBotPipeStore = null!;

    private List<Pipe> _pipes = new();

    private bool _isLoaded = false;

    public PipeManagementPage()
    {
        InitializeComponent();

        pipesListBox.DisplayMember = nameof(Pipe.Name);
    }

    private void InitializeServices()
    {
        _chatBotPipeStore = Services.Get<IPipeStore>();

    }

    private void PipeManagementPage_Load(object sender, EventArgs e)
    {
        if (DesignMode)
        {
            return;
        }

        InitializeServices();

        _isLoaded = true;
    }

    protected override async void OnVisibleChanged(EventArgs e)
    {
        base.OnVisibleChanged(e);

        if (!_isLoaded)
        {
            return;
        }

        _pipes = await _chatBotPipeStore.GetPipesAsync(AppUser.Default);

        if (_pipes.Count == 0)
        {
            await AddTaskTemplateAsync();
        }

        var selectedIndex = pipesListBox.SelectedIndex;

        UpdatePipeList();

        if (selectedIndex >= 0)
        {
            pipesListBox.SetSelected(selectedIndex, true);
        }

        UpdatePipeEditor();
    }

    private async void AddPipeButton_Click(object sender, EventArgs e)
    {
        await AddTaskTemplateAsync();
    }

    private async Task AddTaskTemplateAsync()
    {
        var newPipe = new Pipe(
            new List<PipeTaskTemplateUsage> { },
            "New Pipe");

        await _chatBotPipeStore.AddPipeAsync(AppUser.Default, newPipe);

        _pipes.Add(newPipe);

        UpdatePipeList();

        pipesListBox.SelectedIndex = pipesListBox.Items.Count - 1;
    }

    private void UpdatePipeList()
    {
        pipesListBox.Items.Clear();
        pipesListBox.Items.AddRange(_pipes.ToArray());
    }

    private void PipesListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdatePipeEditor();
    }

    private void UpdatePipeEditor()
    {
        if (pipesListBox.SelectedItem is Pipe selectedPipe)
        {
            pipeEditor.SetPipeToEdit(selectedPipe);
        }

        pipeEditor.Visible = pipesListBox.SelectedItem is not null;
    }

    private async void PipeEditor_PipeUpdated(object sender, Pipe pipe)
    {
        await _chatBotPipeStore.UpdatePipeAsync(AppUser.Default, pipe);

        UpdatePipeList();
    }

    private async void PipeEditor_PipeDeleted(object sender, Pipe pipe)
    {
        await _chatBotPipeStore.RemovePipeAsync(AppUser.Default, pipe);

        _pipes.Remove(pipe);

        pipeEditor.Visible = false;

        UpdatePipeList();
    }
}
