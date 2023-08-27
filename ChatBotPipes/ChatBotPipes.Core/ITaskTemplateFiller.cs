namespace ChatBotPipes.Core;

using ChatBotPipes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITaskTemplateFiller
{
    IEnumerable<string> GetInputs(ChatBotTaskTemplate template);

    ChatBotTask FillInput(ChatBotTaskTemplate template, TaskVariableValueMap inputs);

}
