namespace ChatBotPipes.Client.Implementation;

using ChatBotPipes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ChatBotTaskRunner : IChatBotTaskRunner
{
    private readonly IChatBot _chatBot;

    public ChatBotTaskRunner(IChatBot chatBot)
    {
        _chatBot = chatBot ?? throw new ArgumentNullException(nameof(chatBot));
    }

    public async Task<IChatBotResponse> RunTaskAsync(ChatBotTaskTemplate taskTemplate, string input, ITaskTemplateFiller taskTemplateFiller)
    {
        var task = taskTemplateFiller.FillInput(taskTemplate, input);

        var response = await _chatBot.RespondAsync(task);

        return response;
    }
}
