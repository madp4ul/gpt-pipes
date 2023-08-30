namespace ChatBotPipes.Core.Pipes;
using ChatBotPipes.Core.TaskTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

public record Pipe
{
    public Guid Id { get; }

    public string Name { get; set; }

    public List<PipeTaskTemplateUsage> Tasks { get; }

    public Pipe(List<PipeTaskTemplateUsage> tasks, string name, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        Tasks = tasks;
    }

    public string GetTaskNameInContext(PipeTaskTemplateUsage taskTemplate)
    {
        int taskIndex = Tasks.IndexOf(taskTemplate);

        return $"{taskTemplate.TaskTemplate.Name} (id {taskIndex})";
    }

    public List<PipeTaskTemplateVariableReference> GetRequiredInputs(ITaskTemplateFiller templateFiller)
    {
        var result = new List<PipeTaskTemplateVariableReference>();

        foreach (var mappedTaskTemplate in Tasks)
        {
            var inputs = templateFiller.GetInputs(mappedTaskTemplate.TaskTemplate);

            foreach (string input in inputs)
            {
                if (!HasValidInputMapping(mappedTaskTemplate, input, templateFiller))
                {
                    result.Add(new PipeTaskTemplateVariableReference(mappedTaskTemplate, input));
                }
            }
        }

        return result;
    }

    private bool HasValidInputMapping(PipeTaskTemplateUsage currentTaskTemplate, string currentInput, ITaskTemplateFiller templateFiller)
    {
        if (!currentTaskTemplate.InputMapping.TryGetValue(currentInput, out PipeTaskTemplateVariableReference? referencedVariable))
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

        var variablesOfReferencedTaskTemplate = referencedVariable.TaskTemplate.TaskTemplate.GetReferencibleVariableNames(templateFiller);
        if (!variablesOfReferencedTaskTemplate.Contains(referencedVariable.InputName))
        {
            // The referenced variable does not exist (anymore?) in the referenced task template.
            return false;
        }

        return true;

        bool IsBeforeCurrentTaskTemplate(PipeTaskTemplateUsage t) => t != currentTaskTemplate;
        bool IsReferencedTaskTemplate(PipeTaskTemplateUsage t) => t == referencedVariable?.TaskTemplate;
    }
}