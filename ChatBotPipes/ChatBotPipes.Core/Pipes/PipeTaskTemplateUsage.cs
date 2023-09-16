namespace ChatBotPipes.Core.Pipes;
using ChatBotPipes.Core.TaskTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///     Stores a task template and matches references to variables from previous task templates to its inputs.
///     
///     If an input of the task template does not have a matching reference, its value has to be provided by the user at runtime.
/// </summary>
/// <param name="TaskTemplate">The task template itself.</param>
/// <param name="InputVariableReferences">Variable references. The key in this dictionary is the name of the input in <see cref="TaskTemplate"/>, the value describes where the value is coming from.</param>
public record PipeTaskTemplateUsage(TaskTemplate TaskTemplate, Dictionary<string, PipeTaskTemplateVariableReference> InputVariableReferences);
