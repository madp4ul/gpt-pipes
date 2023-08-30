namespace ChatBotPipes.Client.Implementation;

using ChatBotPipes.Core;
using ChatBotPipes.Core.TaskTemplates;
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

    public async Task<IChatBotResponse> RunTaskAsync(TaskTemplate taskTemplate, TaskTemplateValues inputs, ITaskTemplateFiller taskTemplateFiller, CancellationToken cancellationToken = default)
    {
        var chatBot = _chatBotProvider.CreateChatBot(taskTemplate.ChatBotName);

        var task = taskTemplateFiller.FillInput(taskTemplate, inputs);

        var response = await chatBot.RespondAsync(task, cancellationToken);

        return response;
    }
}
