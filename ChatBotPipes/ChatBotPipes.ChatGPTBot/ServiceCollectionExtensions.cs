namespace ChatBotPipes.ChatGPTBot;

using ChatBotPipes.Client;
using ChatBotPipes.Client.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddChatGpt(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }

    public static IHost UseChatGptBots(this IHost host)
    {
        var provider = host.Services.GetRequiredService<IChatBotProvider>();

        provider.RegisterChatBotFactory("GPT-3.5", () => CreateChatBotWithModel(host.Services, Model.ChatGPTTurbo));
        provider.RegisterChatBotFactory("GPT-4", () => CreateChatBotWithModel(host.Services, Model.GPT4));
        provider.RegisterChatBotFactory("GPT-4o", () => CreateChatBotWithModel(host.Services,
            new Model { ModelID = "GPT-4o" }));

        return host;
    }

    private static IChatBot CreateChatBotWithModel(IServiceProvider serviceProvider, Model model)
        => CreateChatBotWithOptions(serviceProvider, new ChatGptChatBotOptions(new ChatRequest { Model = model }));

    private static IChatBot CreateChatBotWithOptions(IServiceProvider serviceProvider, ChatGptChatBotOptions? options)
        => new ChatGptChatBot(
            serviceProvider.GetRequiredService<IApiKeyStore>(),
            serviceProvider.GetRequiredService<ICurrentUserService>(),
            options);
}
