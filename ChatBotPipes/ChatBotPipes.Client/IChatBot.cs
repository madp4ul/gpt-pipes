namespace ChatBotPipes.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBotPipes.Core.TaskTemplates;

public interface IChatBot
{
    Task<IChatBotResponse> RespondAsync(ChatBotTask task, CancellationToken cancellationToken = default);
}
