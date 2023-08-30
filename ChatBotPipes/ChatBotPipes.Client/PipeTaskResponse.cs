namespace ChatBotPipes.Client;
using ChatBotPipes.Core.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public record PipeTaskResponse(PipeTaskTemplateUsage Task, IChatBotResponse Response);
