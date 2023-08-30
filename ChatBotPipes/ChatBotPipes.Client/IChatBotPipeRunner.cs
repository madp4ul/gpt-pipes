namespace ChatBotPipes.Client;
using ChatBotPipes.Core.Pipes;
using ChatBotPipes.Core.TaskTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IChatBotPipeRunner
{
    IAsyncEnumerable<PipeTaskResponse> RunPipeAsync(Pipe pipe, PipeTemplateValues userInputs, ITaskTemplateFiller taskTemplateFiller, CancellationToken cancellationToken = default);
}
