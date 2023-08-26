namespace ChatBotPipes.Client.Implementation;

using ChatBotPipes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ChatBotTaskRunner : IChatBotTaskRunner
{
    private readonly IChatBotProvider _chatBotProvider;

    public ChatBotTaskRunner(IChatBotProvider chatBotProvider)
    {
        _chatBotProvider = chatBotProvider;
    }

    public async Task<IChatBotResponse> RunTaskAsync(ChatBotTaskTemplate taskTemplate, string input, ITaskTemplateFiller taskTemplateFiller)
    {
        var chatBot = _chatBotProvider.CreateChatBot(taskTemplate.ChatBotName);

        var task = taskTemplateFiller.FillInput(taskTemplate, input);

        var response = await chatBot.RespondAsync(task);

        return response;
    }
}
