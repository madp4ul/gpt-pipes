namespace ChatBotPipes.Core;

using ChatBotPipes.Core.Implementation;
using ChatBotPipes.Core.TaskTemplates;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ITaskTemplateFiller, TaskTemplateFiller>();
        serviceCollection.AddSingleton<ITextInserter, CurlyBraceTextInserter>();

        return serviceCollection;
    }
}
