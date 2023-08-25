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
    public static IServiceCollection AddClientServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IChatBotTaskRunner, ChatBotTaskRunner>();
        serviceCollection.AddSingleton<IChatBotPipeRunner, ChatBotPipeRunner>();
        serviceCollection.AddSingleton<ICurrentUserService, CurrentUserService>();

        return serviceCollection;
    }
}
