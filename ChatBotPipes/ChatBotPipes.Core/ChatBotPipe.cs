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

    public List<ChatBotTaskTemplate> Tasks { get; }

    public ChatBotPipe(List<ChatBotTaskTemplate> tasks, string name, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        Tasks = tasks ?? throw new ArgumentNullException(nameof(tasks));
    }
}

