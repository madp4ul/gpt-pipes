namespace ChatBotPipes.Core.TaskTemplates;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITaskTemplateFiller
{
    IEnumerable<string> GetInputs(TaskTemplate template);

    ChatBotTask FillInput(TaskTemplate template, TaskTemplateValues inputs);

}
