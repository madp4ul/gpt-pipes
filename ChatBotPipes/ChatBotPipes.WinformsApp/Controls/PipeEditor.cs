namespace ChatBotPipes.WinformsApp.Controls;

using ChatBotPipes.Core;
using ChatBotPipes.WinformsApp.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class PipeEditor : UserControl
{
    public event EventHandler<ChatBotPipe>? PipeUpdated;

    public event EventHandler<ChatBotPipe>? PipeDeleted;

    public ChatBotPipe? Pipe { get; private set; }

    public PipeEditor()
    {
        InitializeComponent();
    }

    public void SetPipeToEdit(ChatBotPipe pipe)
    {
        Pipe = pipe;

        nameTextBox.Text = pipe.Name;

        foreach (var control in taskTemplatePanel.Controls.OfType<PipeTaskTemplateControl>())
        {
            UnsubscribeEvents(control);
        }

        taskTemplatePanel.Controls.Clear();

        foreach (var taskTemplate in pipe.Tasks)
        {
            taskTemplatePanel.Controls.Add(CreateTaskTemplateControl(taskTemplate, pipe));
        }
    }

    private void UnsubscribeEvents(PipeTaskTemplateControl control)
    {
        control.InsertAboveRequested -= Control_InsertAboveRequested;
        control.MoveUpRequested -= Control_MoveUpRequested;
        control.MoveDownRequested -= Control_MoveDownRequested;
    }

    private void UpdatePipe(Action<ChatBotPipe> updateAction)
    {
        ArgumentNullException.ThrowIfNull(Pipe);

        updateAction(Pipe);

        PipeUpdated?.Invoke(this, Pipe);
    }

    private void DeletePipeButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(Pipe);

        if (MessageBox.Show("Are you sure you want to delete this pipe?", "Confirm deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            PipeDeleted?.Invoke(this, Pipe);
        }
    }

    private void NameTextBox_Leave(object sender, EventArgs e)
    {
        UpdatePipe(pipe => pipe.Name = nameTextBox.Text);
    }

    private void AddTaskToEndButton_Click(object sender, EventArgs e)
    {
        UpdatePipe(pipe =>
        {
            if (TrySelectTaskTemplate(out var taskTemplate))
            {
                var taskTemplateMapping = new MappedChatBotTaskTemplate(taskTemplate, new Dictionary<string, TaskTemplateVariableName>());

                pipe.Tasks.Add(taskTemplateMapping);

                var control = CreateTaskTemplateControl(taskTemplateMapping, pipe);
                taskTemplatePanel.Controls.Add(control);
            }
        });
    }

    private static bool TrySelectTaskTemplate([NotNullWhen(true)] out ChatBotTaskTemplate? taskTemplate)
    {
        taskTemplate = null;

        var taskSelectionForm = new TaskTemplateSelectionForm();

        if (taskSelectionForm.ShowDialog() == DialogResult.OK
            && taskSelectionForm.SelectedTaskTemplate is not null)
        {
            taskTemplate = taskSelectionForm.SelectedTaskTemplate;
            return true;
        }

        return false;
    }

    private PipeTaskTemplateControl CreateTaskTemplateControl(MappedChatBotTaskTemplate taskTemplate, ChatBotPipe sourcePipe)
    {
        var control = new PipeTaskTemplateControl()
        {
            Width = taskTemplatePanel.Width - 30,
        };

        control.SetTaskTemplate(taskTemplate, sourcePipe);

        control.InsertAboveRequested += Control_InsertAboveRequested;
        control.MoveUpRequested += Control_MoveUpRequested;
        control.MoveDownRequested += Control_MoveDownRequested;
        control.RemoveRequested += Control_RemoveRequested;
        control.InputMappingUpdated += Control_InputMappingUpdated;

        return control;
    }

    private void Control_InputMappingUpdated(object? sender, MappedChatBotTaskTemplate e)
    {
        UpdatePipe(pipe =>
        {
            // pipe is already updated, nothing additional to be done.
        });
    }

    private void Control_MoveUpRequested(object? sender, MappedChatBotTaskTemplate currentTaskTemplate)
    {
        UpdatePipe(pipe =>
        {
            int index = pipe.Tasks.IndexOf(currentTaskTemplate);
            int newIndex = index - 1;

            if (newIndex < 0)
            {
                return; // is first task already
            }

            pipe.Tasks.RemoveAt(index);
            pipe.Tasks.Insert(newIndex, currentTaskTemplate);

            taskTemplatePanel.Controls.SetChildIndex((Control)sender!, newIndex);
        });
    }

    private void Control_MoveDownRequested(object? sender, MappedChatBotTaskTemplate currentTaskTemplate)
    {
        UpdatePipe(pipe =>
        {
            int index = pipe.Tasks.IndexOf(currentTaskTemplate);
            int newIndex = index + 1;

            if (newIndex >= pipe.Tasks.Count)
            {
                return; // is last task already
            }

            pipe.Tasks.RemoveAt(index);
            pipe.Tasks.Insert(newIndex, currentTaskTemplate);

            taskTemplatePanel.Controls.SetChildIndex((Control)sender!, newIndex);
        });
    }

    private void Control_InsertAboveRequested(object? sender, MappedChatBotTaskTemplate currentTaskTemplate)
    {
        UpdatePipe(pipe =>
        {
            int index = pipe.Tasks.IndexOf(currentTaskTemplate);

            if (TrySelectTaskTemplate(out var selectedTaskTemplate))
            {
                var taskTemplateMapping = new MappedChatBotTaskTemplate(selectedTaskTemplate, new Dictionary<string, TaskTemplateVariableName>());

                pipe.Tasks.Insert(index, taskTemplateMapping);

                var control = CreateTaskTemplateControl(taskTemplateMapping, pipe);
                taskTemplatePanel.Controls.Add(control);
                taskTemplatePanel.Controls.SetChildIndex(control, index);
            }
        });
    }

    private void Control_RemoveRequested(object? sender, MappedChatBotTaskTemplate currentTaskTemplate)
    {
        UpdatePipe(pipe =>
        {
            int index = pipe.Tasks.IndexOf(currentTaskTemplate);
            pipe.Tasks.RemoveAt(index);

            var control = (PipeTaskTemplateControl)sender!;
            UnsubscribeEvents(control);

            taskTemplatePanel.Controls.Remove(control);
        });
    }

    private void RunPipeButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(Pipe);

        var runnerForm = new PipeRunnerForm();

        runnerForm.SetPipe(Pipe);

        runnerForm.ShowDialog();
    }
}
