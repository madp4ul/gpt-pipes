namespace ChatBotPipes.WinformsApp.Controls;

using ChatBotPipes.Core.Pipes;
using ChatBotPipes.Core.TaskTemplates;
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
    public event EventHandler<Pipe>? PipeUpdated;

    public event EventHandler<Pipe>? PipeDeleted;

    public Pipe? Pipe { get; private set; }

    public PipeEditor()
    {
        InitializeComponent();
    }

    public void SetPipeToEdit(Pipe pipe)
    {
        Pipe = pipe;

        nameTextBox.Text = pipe.Name;

        foreach (var control in taskTemplatePanel.Rows.OfType<PipeTaskTemplateControl>())
        {
            UnsubscribeEvents(control);
        }

        taskTemplatePanel.ClearRows();

        foreach (var taskTemplate in pipe.Tasks)
        {
            taskTemplatePanel.AddRow(CreateTaskTemplateControl(taskTemplate, pipe));
        }
    }

    private void UnsubscribeEvents(PipeTaskTemplateControl control)
    {
        control.InsertAboveRequested += Control_InsertAboveRequested;
        control.MoveUpRequested += Control_MoveUpRequested;
        control.MoveDownRequested += Control_MoveDownRequested;
        control.RemoveRequested += Control_RemoveRequested;
        control.InputMappingUpdated += Control_InputMappingUpdated;
    }

    private void UpdatePipe(Action<Pipe> updateAction)
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
                var taskTemplateMapping = new PipeTaskTemplateUsage(taskTemplate, new Dictionary<string, PipeTaskTemplateVariableReference>());

                pipe.Tasks.Add(taskTemplateMapping);

                var control = CreateTaskTemplateControl(taskTemplateMapping, pipe);
                taskTemplatePanel.AddRow(control);
            }
        });
    }

    private static bool TrySelectTaskTemplate([NotNullWhen(true)] out TaskTemplate? taskTemplate)
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

    private PipeTaskTemplateControl CreateTaskTemplateControl(PipeTaskTemplateUsage taskTemplate, Pipe sourcePipe)
    {
        var control = new PipeTaskTemplateControl();

        control.SetTaskTemplate(taskTemplate, sourcePipe);

        control.InsertAboveRequested += Control_InsertAboveRequested;
        control.MoveUpRequested += Control_MoveUpRequested;
        control.MoveDownRequested += Control_MoveDownRequested;
        control.RemoveRequested += Control_RemoveRequested;
        control.InputMappingUpdated += Control_InputMappingUpdated;

        return control;
    }

    private void Control_InputMappingUpdated(object? sender, PipeTaskTemplateUsage e)
    {
        UpdatePipe(pipe =>
        {
            // pipe is already updated, nothing additional to be done.
        });
    }

    private void Control_MoveUpRequested(object? sender, PipeTaskTemplateUsage currentTaskTemplate)
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

            taskTemplatePanel.SetRowIndex((Control)sender!, newIndex);
        });
    }

    private void Control_MoveDownRequested(object? sender, PipeTaskTemplateUsage currentTaskTemplate)
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

            taskTemplatePanel.SetRowIndex((Control)sender!, newIndex);
        });
    }

    private void Control_InsertAboveRequested(object? sender, PipeTaskTemplateUsage currentTaskTemplate)
    {
        UpdatePipe(pipe =>
        {
            int index = pipe.Tasks.IndexOf(currentTaskTemplate);

            if (TrySelectTaskTemplate(out var selectedTaskTemplate))
            {
                var taskTemplateMapping = new PipeTaskTemplateUsage(selectedTaskTemplate, new Dictionary<string, PipeTaskTemplateVariableReference>());

                pipe.Tasks.Insert(index, taskTemplateMapping);

                var control = CreateTaskTemplateControl(taskTemplateMapping, pipe);
                taskTemplatePanel.AddRow(control);
                taskTemplatePanel.SetRowIndex(control, index);
            }
        });
    }

    private void Control_RemoveRequested(object? sender, PipeTaskTemplateUsage currentTaskTemplate)
    {
        UpdatePipe(pipe =>
        {
            var control = (PipeTaskTemplateControl)sender!;

            int index = pipe.Tasks.IndexOf(currentTaskTemplate);
            pipe.Tasks.RemoveAt(index);

            foreach (var task in pipe.Tasks)
            {
                var invalidVariableReferences = task.InputVariableReferences
                    .Where(kv => !pipe.Tasks.Contains(kv.Value.ReferencedTaskTemplate))
                    .ToList();

                invalidVariableReferences.ForEach(r => task.InputVariableReferences.Remove(r.Key));

                var referencingControl = GetControlForTaskUsage(task);
                referencingControl?.SetTaskTemplate(task, pipe); // Trigger update of task values because the input mapping has changed.
            }

            UnsubscribeEvents(control);

            taskTemplatePanel.RemoveRow(control);
        });
    }

    private PipeTaskTemplateControl? GetControlForTaskUsage(PipeTaskTemplateUsage taskTemplateUsage)
        => taskTemplatePanel.Rows
            .OfType<PipeTaskTemplateControl>()
            .FirstOrDefault(c => c.TaskTemplateMapping == taskTemplateUsage);

    private void RunPipeButton_Click(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(Pipe);

        var runnerForm = new PipeRunnerForm();

        runnerForm.SetPipe(Pipe);

        runnerForm.ShowDialog();
    }
}
