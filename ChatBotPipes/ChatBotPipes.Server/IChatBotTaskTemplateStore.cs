namespace ChatBotPipes.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IChatBotTaskTemplateStore
{
    Task<List<ChatBotTaskTemplate>> GetTaskTemplatesAsync(User user);

    Task AddTaskTemplateAsync(User user, ChatBotTaskTemplate task);

    Task RemoveTaskTemplateAsync(User user, ChatBotTaskTemplate task);

    Task UpdateTaskTemplateAsync(User user, ChatBotTaskTemplate task);
}
