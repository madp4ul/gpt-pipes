namespace ChatBotPipes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public record ChatBotPipe
{
    public Guid Id { get; }

    public string Name { get; set; }

    public List<MappedChatBotTaskTemplate> Tasks { get; }

    public ChatBotPipe(List<MappedChatBotTaskTemplate> tasks, string name, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        Tasks = tasks;
    }

    public List<TaskTemplateVariableName> GetRequiredInputs(ITaskTemplateFiller templateFiller)
    {
        var result = new List<TaskTemplateVariableName>();

        foreach (var mappedTaskTemplate in Tasks)
        {
            var inputs = templateFiller.GetInputs(mappedTaskTemplate.TaskTemplate);

            foreach (string input in inputs)
            {
                if (!HasValidInputMapping(mappedTaskTemplate, input, templateFiller))
                {
                    result.Add(new TaskTemplateVariableName(mappedTaskTemplate.TaskTemplate, input));
                }
            }
        }

        return result;
    }

    private bool HasValidInputMapping(MappedChatBotTaskTemplate currentTaskTemplate, string currentInput, ITaskTemplateFiller templateFiller)
    {
        if (!currentTaskTemplate.InputMapping.TryGetValue(currentInput, out TaskTemplateVariableName? referencedVariable))
        {
            // There is no input mapping for the given input
            return false;
        }

        bool referencedTemplateIsBeforeCurrent = Tasks
            .TakeWhile(IsBeforeCurrentTaskTemplate)
            .Any(IsReferencedTaskTemplate);

        if (!referencedTemplateIsBeforeCurrent)
        {
            // There is an input mapping, but the references task template does not come before the current task in the pipe (anymore?).
            return false;

        }

        var variablesOfReferencedTaskTemplate = referencedVariable.TaskTemplate.GetReferencibleVariableNames(templateFiller);
        if (!variablesOfReferencedTaskTemplate.Contains(referencedVariable.InputName))
        {
            // The referenced variable does not exist (anymore?) in the referenced task template.
            return false;
        }

        return true;

        bool IsBeforeCurrentTaskTemplate(MappedChatBotTaskTemplate t) => t.TaskTemplate != currentTaskTemplate.TaskTemplate;
        bool IsReferencedTaskTemplate(MappedChatBotTaskTemplate t) => t.TaskTemplate == referencedVariable?.TaskTemplate;
    }
}