namespace ChatBotPipes.Client.Implementation;

using ChatBotPipes.Core;
using ChatBotPipes.Core.Pipes;
using ChatBotPipes.Core.TaskTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public class ChatBotPipeRunner : IChatBotPipeRunner
{
    private readonly IChatBotTaskRunner _taskRunner;

    public ChatBotPipeRunner(IChatBotTaskRunner taskRunner)
    {
        _taskRunner = taskRunner ?? throw new ArgumentNullException(nameof(taskRunner));
    }

    public RunPipeResult RunPipeAsync(Pipe pipe, PipeTemplateValues userInputs, ITaskTemplateFiller taskTemplateFiller, CancellationToken cancellationToken = default)
    {
        //var variableValues = userInputs.CopyMap(); // Copy to not modify users instance of their inputs.

        // If problems with parallelized running occur, switch back to sequencial approach. --> done.
        var taskResponses = RunPipeSequenciallyAsync(pipe, userInputs, taskTemplateFiller, cancellationToken);

        return new RunPipeResult(taskResponses, userInputs);
    }

    private async IAsyncEnumerable<PipeTaskResponse> RunPipeParallelizedAsync(Pipe pipe, PipeTemplateValues variableValues, ITaskTemplateFiller taskTemplateFiller, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        List<PipeTaskTemplateUsage> tasksWithMissingInputs = pipe.Tasks.ToList();

        List<Task<PipeTaskResponse?>> runningTasks = new();

        while (tasksWithMissingInputs.Count > 0 || runningTasks.Count > 0)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var readyTaskUsages = tasksWithMissingInputs
                .Where(t => HasAllInputsProvided(variableValues, t))
                .ToList();

            tasksWithMissingInputs.RemoveAll(readyTaskUsages.Contains);

            var newReadyChatBotTasks = readyTaskUsages
                .Select(t => RunTaskTemplate(t, variableValues))
                .ToList();
            runningTasks.AddRange(newReadyChatBotTasks);

            var completedTask = await Task.WhenAny(runningTasks);

            cancellationToken.ThrowIfCancellationRequested();

            runningTasks.Remove(completedTask);

            var response = await completedTask;

            if (response is not null)
            {
                yield return response;

                runningTasks.Add(AddOutputAfterCompletionAsync(response));
            }
        }

        async Task<PipeTaskResponse?> RunTaskTemplate(PipeTaskTemplateUsage taskTemplateUsage, PipeTemplateValues variableValues)
        {
            TaskTemplateValues taskVariableValues = GetTaskVariableValues(variableValues, taskTemplateUsage);

            var response = await _taskRunner.RunTaskAsync(taskTemplateUsage.TaskTemplate, taskVariableValues, taskTemplateFiller, cancellationToken);

            return new PipeTaskResponse(taskTemplateUsage, response);
        }

        async Task<PipeTaskResponse?> AddOutputAfterCompletionAsync(PipeTaskResponse pipeResponse)
        {
            string output = await pipeResponse.Response.AwaitCompletionAsync();

            variableValues.SetOutputValue(pipeResponse.Task, output);

            return null; // We also have to await this, but this does not produce a value to be yielded.
        }
    }

    public async IAsyncEnumerable<PipeTaskResponse> RunPipeSequenciallyAsync(Pipe pipe, PipeTemplateValues userInputs, ITaskTemplateFiller taskTemplateFiller, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var task in pipe.Tasks)
        {
            TaskTemplateValues taskVariableValues = GetTaskVariableValues(userInputs, task);

            if (taskVariableValues.HasOutput())
            {
                continue;
            }

            var response = await _taskRunner.RunTaskAsync(task.TaskTemplate, taskVariableValues, taskTemplateFiller, cancellationToken);

            yield return new PipeTaskResponse(task, response);

            try
            {
                await response.AwaitCompletionAsync();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            finally
            {
                taskVariableValues.AddOutputValue(response.GetCurrentResponse());
            }
        }
    }

    private static bool HasAllInputsProvided(PipeTemplateValues variableValues, PipeTaskTemplateUsage task)
        => task.InputVariableReferences.All(i => variableValues.Has(i.Value));

    private static TaskTemplateValues GetTaskVariableValues(PipeTemplateValues variableValues, PipeTaskTemplateUsage task)
    {
        // take user inputs and add values from referenced variables in the pipe to them.

        TaskTemplateValues taskValueMap = variableValues.Get(task);

        foreach (var (inputName, VariableReference) in task.InputVariableReferences)
        {
            string referencedValue = variableValues.Get(VariableReference);

            taskValueMap.AddInputValue(inputName, referencedValue);
        }

        return taskValueMap;
    }
}
