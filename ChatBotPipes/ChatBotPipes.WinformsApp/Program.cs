namespace ChatBotPipes.WinformsApp;

using ChatBotPipes.ChatGPTBot;
using ChatBotPipes.Client;
using ChatBotPipes.Core;
using ChatBotPipes.LocalFileStores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static async Task Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureServices)
            .Build();

        await InitializeHostAsync(host);

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(Services.Get<MainWindow>());
    }

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services
            .AddCoreServices()
            .AddClientServices()
            .AddLocalFileStores()
            .AddChatGpt();

        services.AddTransient<MainWindow>();
    }

    private static async Task InitializeHostAsync(IHost host)
    {
        var currentUserService = host.Services.GetRequiredService<ICurrentUserService>();

        await currentUserService.SetCurrentUserAsync(AppUser.Default);

        host.UseChatGptBots();


        Services.SetProvider(host.Services);
    }
}