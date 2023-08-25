namespace ChatBotPipes.Client;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public record ChatBotTaskCompletion
{
    public ChatBotTask Task { get; }

    public ChatBotTaskCompletion(ChatBotTask task)
    {
        Task = task ?? throw new ArgumentNullException(nameof(task));
    }

    public async Task<IChatBotResponse> CreateCompletionAsync(IChatBot chatBot)
    {
        var completion = await chatBot.RespondAsync(Task);

        return completion;
    }
}
