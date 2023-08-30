namespace ChatBotPipes.Client;
using ChatBotPipes.Core.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IPipeStore
{
    Task<List<Pipe>> GetPipesAsync(User user);

    Task AddPipeAsync(User user, Pipe pipe);

    Task RemovePipeAsync(User user, Pipe pipe);

    Task UpdatePipeAsync(User user, Pipe pipe);
}
