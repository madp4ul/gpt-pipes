namespace ChatBotPipes.Client;

using ChatBotPipes.Client.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClientServices(this IServiceCollection serviceCollection) => serviceCollection
        .AddSingleton<IChatBotTaskRunner, ChatBotTaskRunner>()
        .AddSingleton<IChatBotPipeRunner, ChatBotPipeRunner>()
        .AddSingleton<ICurrentUserService, CurrentUserService>()
        .AddSingleton<IChatBotProvider, ChatBotProvider>();
}
