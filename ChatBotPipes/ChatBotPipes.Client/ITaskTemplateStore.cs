namespace ChatBotPipes.Client;
using ChatBotPipes.Core.TaskTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITaskTemplateStore
{
    Task<List<TaskTemplate>> GetTaskTemplatesAsync(User user);

    Task AddTaskTemplateAsync(User user, TaskTemplate task);

    Task RemoveTaskTemplateAsync(User user, TaskTemplate task);

    Task UpdateTaskTemplateAsync(User user, TaskTemplate task);
}
