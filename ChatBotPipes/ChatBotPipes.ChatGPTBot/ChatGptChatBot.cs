namespace ChatBotPipes.ChatGPTBot;

using ChatBotPipes.Client;
using ChatBotPipes.Core;
using OpenAI_API.Chat;
using System.Threading.Tasks;

public class ChatGptChatBot : IChatBot
{
    private readonly IApiKeyStore _apiKeyStore;
    private readonly ICurrentUserService _currentUserService;
    private readonly ChatGptChatBotOptions? _options;

    private readonly OpenAI_API.OpenAIAPI _api;

    public ChatGptChatBot(IApiKeyStore apiKeyStore, ICurrentUserService currentUserService, ChatGptChatBotOptions? options = null)
    {
        _apiKeyStore = apiKeyStore;
        _currentUserService = currentUserService;
        _options = options;

        _api = new OpenAI_API.OpenAIAPI();
    }

    public async Task<IChatBotResponse> RespondAsync(ChatBotTask task)
    {
        await SetAuthenticationAsync();

        var syncContext = SynchronizationContext.Current ?? throw new ApplicationException("Synchronization.Current must be set. If this is running as part of a UI-app, it should have been set and something might be wrong, otherwise set it manually with SynchronizationContext.SetSynchronizationContext.");

        var response = new ChatBotResponse(syncContext);

        _ = Task.Run(async () => await StreamResponseUpdatesAsync(task, response));

        return response;
    }

    private async Task StreamResponseUpdatesAsync(ChatBotTask task, ChatBotResponse chatBotResponse)
    {
        Conversation conversation = CreateConversation(task);

        var responseEnumerable = conversation.StreamResponseEnumerableFromChatbotAsync();

        await foreach (string update in responseEnumerable)
        {
            chatBotResponse.AppendData(update);
        }

        chatBotResponse.MarkAsComplete();
    }

    private Conversation CreateConversation(ChatBotTask task)
    {
        var conversation = _api.Chat.CreateConversation(_options?.ChatRequest);

        foreach (var instruction in task.PredefinedInstructions)
        {
            var role = GetRole(instruction);

            var message = new OpenAI_API.Chat.ChatMessage(role, instruction.Text);

            conversation.AppendMessage(message);
        }

        return conversation;
    }

    private static ChatMessageRole GetRole(Core.ChatMessage instruction)
    {
        return instruction.Author switch
        {
            ChatMessageAuthor.System => ChatMessageRole.System,
            ChatMessageAuthor.User => ChatMessageRole.User,
            ChatMessageAuthor.Bot => ChatMessageRole.Assistant,
            _ => throw new NotImplementedException("Unknown " + nameof(ChatMessageAuthor))
        };
    }

    private async Task SetAuthenticationAsync()
    {
        var apiKey = await _apiKeyStore.GetApiKeyAsync()
            ?? throw new InvalidOperationException("Current user does not have api key set");

        _api.Auth = new OpenAI_API.APIAuthentication(apiKey.Value);
    }
}
