namespace ChatBotPipes.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IChatBotTaskRunner
{
    Task<IChatBotResponse> RunTaskAsync(ChatBotTaskTemplate taskTemplate, TaskVariableValueMap inputs, ITaskTemplateFiller taskTemplateFiller, CancellationToken cancellationToken = default);
}
