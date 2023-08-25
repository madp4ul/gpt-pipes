namespace ChatBotPipes.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ICurrentUserService
{
    Task<User?> GetCurrentUserAsync();

    Task SetCurrentUserAsync(User? user);
}
