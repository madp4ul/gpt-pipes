namespace ChatBotPipes.Core.TaskTemplates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public record ChatMessage
{
    public string Text { get; set; }
    public ChatMessageAuthor Author { get; set; }

    public ChatMessage(string text, ChatMessageAuthor author)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        Author = author;
    }
}
