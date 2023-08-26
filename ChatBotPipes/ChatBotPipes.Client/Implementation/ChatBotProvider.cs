namespace ChatBotPipes.Client.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ChatBotProvider : IChatBotProvider
{
    private readonly Dictionary<string, Func<IChatBot>> _chatBots = new();

    public IEnumerable<string> GetBotNames()
    {
        return _chatBots.Keys;
    }

    public IChatBot CreateChatBot(string? name = null)
    {
        if (string.IsNullOrEmpty(name))
        {
            name = GetDefaultBotName();
        }

        if (!_chatBots.ContainsKey(name))
        {
            throw new InvalidOperationException($"Can not get a chat bot for name {name}, as none has been registered.");
        }

        return _chatBots[name]();
    }

    public void RegisterChatBotFactory(string name, Func<IChatBot> chatBot)
    {
        if (name is "")
        {
            throw new InvalidOperationException("Can not register chat bot for empty string");
        }

        if (!_chatBots.TryAdd(name, chatBot))
        {
            throw new InvalidOperationException($"Can not register multiple chatbots for name '{name}'.");
        }
    }

    public string GetDefaultBotName()
        => _chatBots.Keys.First();
}
