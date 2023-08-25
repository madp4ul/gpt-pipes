namespace ChatBotPipes.LocalFileStores;

using ChatBotPipes.Client;
using ChatBotPipes.Core.Implementation;
using ChatBotPipes.LocalFileStores.Implementation;
using ChatBotPipes.Server;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLocalFileStores(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IAppDataFileService, AppDataFileService>();
        serviceCollection.AddSingleton<IUserDataFileService, UserDataFileService>();

        serviceCollection.AddSingleton<IApiKeyStore, FilebasedApiKeyStore>();
        serviceCollection.AddSingleton<IChatBotTaskTemplateStore, FilebasedChatBotTaskTemplateStore>();
        serviceCollection.AddSingleton<IChatBotPipeStore, FilebasedChatBotPipeStore>();

        return serviceCollection;
    }
}
