namespace ChatBotPipes.WinformsApp;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Services
{
    private static IServiceProvider ServiceProvider { get; set; } = null!;

    public static void SetProvider(IServiceProvider serviceProvider)
        => ServiceProvider = serviceProvider;

    public static TService Get<TService>() where TService : notnull
        => ServiceProvider.GetRequiredService<TService>();
}
