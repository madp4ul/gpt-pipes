namespace ChatBotPipes.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IChatBotResponse
{
    event Action<string> DataReceived;

    string GetCurrentResponse();

    Task<string> AwaitCompletionAsync();
}
