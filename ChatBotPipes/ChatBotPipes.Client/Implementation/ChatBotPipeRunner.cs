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

    public async IAsyncEnumerable<IChatBotResponse> RunPipeAsync(ChatBotPipe pipe, PipeVariableValueMap userInputs, ITaskTemplateFiller taskTemplateFiller)
    {
        var variableValues = userInputs.CopyMap(); // Copy to not modify users instance of their inputs.

        foreach (var task in pipe.Tasks)
        {
            TaskVariableValueMap taskVariableValues = GetTaskVariableValues(variableValues, task);

            var response = await _taskRunner.RunTaskAsync(task.TaskTemplate, taskVariableValues, taskTemplateFiller);

            yield return response;

            var output = await response.AwaitCompletionAsync();

            taskVariableValues.AddOutputValue(output);
        }
    }

    private static TaskVariableValueMap GetTaskVariableValues(PipeVariableValueMap variableValues, MappedChatBotTaskTemplate task)
    {
        // take user inputs and add values from referenced variables in the pipe to them.

        TaskVariableValueMap taskValueMap = variableValues.Get(task.TaskTemplate);

        foreach (var (inputName, VariableReference) in task.InputMapping)
        {
            string referencedValue = variableValues.Get(VariableReference);

            taskValueMap.AddInputValue(inputName, referencedValue);
        }

        return taskValueMap;
    }
}
