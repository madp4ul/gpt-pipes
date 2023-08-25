﻿namespace ChatBotPipes.ChatGPTBot;

using ChatBotPipes.Client;
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
    public static IServiceCollection AddChatGpt(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IChatBot, ChatGptChatBot>();

        return serviceCollection;
    }
}
