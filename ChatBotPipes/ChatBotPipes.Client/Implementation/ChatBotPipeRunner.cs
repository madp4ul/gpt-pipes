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

    public async IAsyncEnumerable<IChatBotResponse> RunPipeAsync(ChatBotPipe pipe, string input, ITaskTemplateFiller taskTemplateFiller)
    {
        string nextInput = input;

        foreach (var task in pipe.Tasks)
        {
            var response = await _taskRunner.RunTaskAsync(task, nextInput, taskTemplateFiller);

            yield return response;

            nextInput = await response.AwaitCompletionAsync();
        }
    }
}
