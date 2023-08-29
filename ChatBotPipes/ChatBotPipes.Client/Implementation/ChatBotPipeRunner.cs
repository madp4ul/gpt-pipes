namespace ChatBotPipes.Client.Implementation;

using ChatBotPipes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ChatBotPipeRunner : IChatBotPipeRunner
{
    private readonly IChatBotTaskRunner _taskRunner;

    public ChatBotPipeRunner(IChatBotTaskRunner taskRunner)
    {
        _taskRunner = taskRunner ?? throw new ArgumentNullException(nameof(taskRunner));
    }

    public IAsyncEnumerable<ChatBotPipeResponse> RunPipeAsync(ChatBotPipe pipe, PipeVariableValueMap userInputs, ITaskTemplateFiller taskTemplateFiller)
    {
        var variableValues = userInputs.CopyMap(); // Copy to not modify users instance of their inputs.

        // If problems with parallelized running occur, switch back to sequencial approach.
        return RunPipeParallelizedAsync(pipe, variableValues, taskTemplateFiller);
    }

    private async IAsyncEnumerable<ChatBotPipeResponse> RunPipeParallelizedAsync(ChatBotPipe pipe, PipeVariableValueMap variableValues, ITaskTemplateFiller taskTemplateFiller)
    {
        List<MappedChatBotTaskTemplate> tasksWithMissingInputs = pipe.Tasks.ToList();

        List<Task<ChatBotPipeResponse?>> runningTasks = new();

        while (tasksWithMissingInputs.Count > 0 || runningTasks.Count > 0)
        {
            var readyTaskUsages = tasksWithMissingInputs
                .Where(t => HasAllInputsProvided(variableValues, t))
                .ToList();

            tasksWithMissingInputs.RemoveAll(readyTaskUsages.Contains);

            var newReadyChatBotTasks = readyTaskUsages
                .Select(t => RunTaskTemplate(t, variableValues))
                .ToList();
            runningTasks.AddRange(newReadyChatBotTasks);

            var completedTask = await Task.WhenAny(runningTasks);

            runningTasks.Remove(completedTask);

            var response = await completedTask;

            if (response is not null)
            {
                yield return response;

                runningTasks.Add(AddOutputAfterCompletionAsync(response));
            }
        }

        async Task<ChatBotPipeResponse?> RunTaskTemplate(MappedChatBotTaskTemplate taskTemplateUsage, PipeVariableValueMap variableValues)
        {
            TaskVariableValueMap taskVariableValues = GetTaskVariableValues(variableValues, taskTemplateUsage);

            var response = await _taskRunner.RunTaskAsync(taskTemplateUsage.TaskTemplate, taskVariableValues, taskTemplateFiller);

            return new ChatBotPipeResponse(taskTemplateUsage, response);
        }

        async Task<ChatBotPipeResponse?> AddOutputAfterCompletionAsync(ChatBotPipeResponse pipeResponse)
        {
            string output = await pipeResponse.Response.AwaitCompletionAsync();

            variableValues.AddOutputValue(pipeResponse.Task, output);

            return null; // We also have to await this, but this does not produce a value to be yielded.
        }
    }

    public async IAsyncEnumerable<ChatBotPipeResponse> RunPipeSequenciallyAsync(ChatBotPipe pipe, PipeVariableValueMap userInputs, ITaskTemplateFiller taskTemplateFiller)
    {
        var variableValues = userInputs.CopyMap(); // Copy to not modify users instance of their inputs.

        foreach (var task in pipe.Tasks)
        {
            TaskVariableValueMap taskVariableValues = GetTaskVariableValues(variableValues, task);

            var response = await _taskRunner.RunTaskAsync(task.TaskTemplate, taskVariableValues, taskTemplateFiller);

            yield return new ChatBotPipeResponse(task, response);

            var output = await response.AwaitCompletionAsync();

            taskVariableValues.AddOutputValue(output);
        }
    }

    private static bool HasAllInputsProvided(PipeVariableValueMap variableValues, MappedChatBotTaskTemplate task)
        => task.InputMapping.All(i => variableValues.Has(i.Value));

    private static TaskVariableValueMap GetTaskVariableValues(PipeVariableValueMap variableValues, MappedChatBotTaskTemplate task)
    {
        // take user inputs and add values from referenced variables in the pipe to them.

        TaskVariableValueMap taskValueMap = variableValues.Get(task);

        foreach (var (inputName, VariableReference) in task.InputMapping)
        {
            string referencedValue = variableValues.Get(VariableReference);

            taskValueMap.AddInputValue(inputName, referencedValue);
        }

        return taskValueMap;
    }
}
