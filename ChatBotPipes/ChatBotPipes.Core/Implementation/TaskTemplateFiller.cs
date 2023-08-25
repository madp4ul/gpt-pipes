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

    public ChatBotTask FillInput(ChatBotTaskTemplate template, string input)
    {
        var messagesWithInput = template.PredefinedInstructions
            .Select(WithInput)
            .ToList();

        return new ChatBotTask(messagesWithInput);

        ChatMessage WithInput(ChatMessage message)
            => message with { Text = _textInserter.Insert(message.Text, input) };
    }
}
