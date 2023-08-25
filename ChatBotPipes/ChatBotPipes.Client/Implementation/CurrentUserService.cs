namespace ChatBotPipes.Client.Implementation;

using ChatBotPipes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class CurrentUserService : ICurrentUserService
{
    private User? _currentUser;

    public Task<User?> GetCurrentUserAsync()
    {
        return Task.FromResult(_currentUser);
    }

    public Task SetCurrentUserAsync(User? user)
    {
        _currentUser = user;

        return Task.CompletedTask;
    }
}
