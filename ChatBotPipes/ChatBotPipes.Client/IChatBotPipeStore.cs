namespace ChatBotPipes.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IChatBotPipeStore
{
    Task<List<ChatBotPipe>> GetPipesAsync(User user);

    Task AddPipeAsync(User user, ChatBotPipe pipe);

    Task RemovePipeAsync(User user, ChatBotPipe pipe);

    Task UpdatePipeAsync(User user, ChatBotPipe pipe);
}
