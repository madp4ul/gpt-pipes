namespace ChatBotPipes.Client.Implementation;

public interface IChatBotProvider
{
    IEnumerable<string> GetBotNames();

    string GetDefaultBotName();

    void RegisterChatBotFactory(string name, Func<IChatBot> chatBot);

    public IChatBot CreateChatBot(string? name = null);
}