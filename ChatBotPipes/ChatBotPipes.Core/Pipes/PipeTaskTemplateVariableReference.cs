namespace ChatBotPipes.Core.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///     References a variable that is defined in context of a specific task template.
/// </summary>
public record PipeTaskTemplateVariableReference(PipeTaskTemplateUsage TaskTemplate, string InputName);