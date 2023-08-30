namespace ChatBotPipes.Client;
using ChatBotPipes.Core.TaskTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IChatBotTaskRunner
{
    Task<IChatBotResponse> RunTaskAsync(TaskTemplate taskTemplate, TaskTemplateValues inputs, ITaskTemplateFiller taskTemplateFiller, CancellationToken cancellationToken = default);
}
