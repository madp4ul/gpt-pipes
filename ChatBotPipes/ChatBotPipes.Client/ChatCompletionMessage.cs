namespace ChatBotPipes.Client;

using ChatBotPipes.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public record ChatCompletionMessage
{
    private readonly IChatBot _chatBot;
    private readonly ChatBotTask _task;

    public ChatMessageAuthor Author => ChatMessageAuthor.Bot;

    public ChatCompletionMessage(ChatBotTask task, IChatBot chatBot)
    {
        _task = task ?? throw new ArgumentNullException(nameof(task));
        _chatBot = chatBot ?? throw new ArgumentNullException(nameof(chatBot));
    }

    public async Task<IChatBotResponse> CreateCompletionAsync()
    {
        var completion = await _chatBot.RespondAsync(_task);

        return completion;
    }
}
