namespace ChatBotPipes.Core.TaskTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public record TaskTemplate
{
    public Guid Id { get; init; }

    public string Name { get; set; }

    public string? ChatBotName { get; set; }

    public List<ChatMessage> PredefinedInstructions { get; init; }

    public TaskTemplate(List<ChatMessage> predefinedInstructions, string name, string? chatBotName, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        ChatBotName = chatBotName;
        PredefinedInstructions = predefinedInstructions ?? throw new ArgumentNullException(nameof(predefinedInstructions));
    }

    public TaskTemplate()
        : this(new List<ChatMessage>(), "", null, Guid.Empty)
    { }

    public IEnumerable<string> GetReferencibleVariableNames(ITaskTemplateFiller templateFiller)
    {
        var inputs = templateFiller.GetInputs(this);

        var allVariables = inputs.Append(TaskTemplateValues.OutputKey);

        return allVariables;
    }
}
