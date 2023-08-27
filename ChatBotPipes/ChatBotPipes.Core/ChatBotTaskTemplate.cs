namespace ChatBotPipes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public record ChatBotTaskTemplate
{
    public Guid Id { get; init; }

    public string Name { get; set; }

    public string? ChatBotName { get; set; }

    public List<ChatMessage> PredefinedInstructions { get; init; }

    public ChatBotTaskTemplate(List<ChatMessage> predefinedInstructions, string name, string? chatBotName, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        ChatBotName = chatBotName;
        PredefinedInstructions = predefinedInstructions ?? throw new ArgumentNullException(nameof(predefinedInstructions));
    }

    public ChatBotTaskTemplate()
        : this(new List<ChatMessage>(), "", null, Guid.Empty)
    { }

    public IEnumerable<string> GetReferencibleVariableNames(ITaskTemplateFiller templateFiller)
    {
        var inputs = templateFiller.GetInputs(this);

        var allVariables = inputs.Append(TaskVariableValueMap.OutputKey);

        return allVariables;
    }
}
