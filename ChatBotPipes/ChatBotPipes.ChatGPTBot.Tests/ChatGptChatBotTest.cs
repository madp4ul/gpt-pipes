namespace ChatBotPipes.ChatGPTBot.Tests;

using ChatBotPipes.Client;
using ChatBotPipes.Core;
using Moq;
using NUnit.Framework;

public class ChatGptChatBotTest
{
    private const string ApiKey = "...";

    private Mock<IApiKeyStore> _apiKeyStoreMock;
    private Mock<ICurrentUserService> _currentUserServiceMock;
    private ChatGptChatBot _sut;

    [SetUp]
    public void Setup()
    {
        SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

        _apiKeyStoreMock = new Mock<IApiKeyStore>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();

        _sut = new ChatGptChatBot(_apiKeyStoreMock.Object, _currentUserServiceMock.Object);
    }

    [Test]
    [Ignore("Integration test, costs money to run")]
    public async Task RespondAsync_IntegrationTest()
    {
        _currentUserServiceMock.Setup(m => m.GetCurrentUserAsync())
            .ReturnsAsync(new User("abc"));

        _apiKeyStoreMock.Setup(m => m.GetApiKeyAsync())
            .ReturnsAsync(new ApiKey(ApiKey));

        var task = new ChatBotTask(new List<ChatMessage>
        {
            new ChatMessage("You are a helpful assistant", ChatMessageAuthor.System),
            new ChatMessage("abc", ChatMessageAuthor.User)
        });

        var response = await _sut.RespondAsync(task);

        string result = await response.AwaitCompletionAsync();

        TestContext.Out.WriteLine("Bot responded with: " + result);
    }
}