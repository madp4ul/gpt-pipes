namespace ChatBotPipes.Core.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TaskTemplateFiller : ITaskTemplateFiller
{
    private readonly ITextInserter _textInserter;

    public TaskTemplateFiller(ITextInserter textInserter)
    {
        _textInserter = textInserter ?? throw new ArgumentNullException(nameof(textInserter));
    }

    public IEnumerable<string> GetInputs(ChatBotTaskTemplate template)
    {
        var allInputs = new HashSet<string>();

        foreach (var message in template.PredefinedInstructions)
        {
            var chatMessageInputs = _textInserter.GetInputs(message.Text);

            foreach (var chatMessageInput in chatMessageInputs)
            {
                allInputs.Add(chatMessageInput);
            }
        }

        return allInputs;
    }

    public ChatBotTask FillInput(ChatBotTaskTemplate template, TaskVariableValueMap inputs)
    {
        var messagesWithInput = template.PredefinedInstructions
            .Select(WithInput)
            .ToList();

        return new ChatBotTask(messagesWithInput);

        ChatMessage WithInput(ChatMessage message)
            => message with { Text = _textInserter.Insert(message.Text, inputs) };
    }
}
