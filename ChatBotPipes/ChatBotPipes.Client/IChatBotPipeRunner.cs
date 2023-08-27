namespace ChatBotPipes.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IChatBotPipeRunner
{
    IAsyncEnumerable<IChatBotResponse> RunPipeAsync(ChatBotPipe pipe, PipeVariableValueMap userInputs, ITaskTemplateFiller taskTemplateFiller);
}
