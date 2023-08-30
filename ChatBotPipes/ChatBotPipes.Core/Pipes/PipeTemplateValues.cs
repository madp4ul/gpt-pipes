namespace ChatBotPipes.Core.Pipes;
using ChatBotPipes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PipeTemplateValues
{
    private readonly Dictionary<PipeTaskTemplateUsage, TaskTemplateValues> _mapping = new();

    public IEnumerable<PipeTaskTemplateVariableReference> MappedInputs => _mapping
        .SelectMany(kv => kv.Value.MappedInputs.Select(mi => new PipeTaskTemplateVariableReference(kv.Key, mi)))
        .ToList();

    public void AddInputValue(PipeTaskTemplateVariableReference templateVariable, string value)
    {
        var taskValues = GetTaskValues(templateVariable.TaskTemplate);

        taskValues.AddInputValue(templateVariable.InputName, value);
    }

    public void AddOutputValue(PipeTaskTemplateUsage taskTemplate, string value)
    {
        var taskValues = GetTaskValues(taskTemplate);

        taskValues.AddOutputValue(value);
    }

    public TaskTemplateValues Get(PipeTaskTemplateUsage taskTemplate)
    {
        return GetTaskValues(taskTemplate);
    }

    private TaskTemplateValues GetTaskValues(PipeTaskTemplateUsage taskTemplate)
    {
        if (!_mapping.ContainsKey(taskTemplate))
        {
            _mapping[taskTemplate] = new TaskTemplateValues();
        }

        return _mapping[taskTemplate];
    }

    public string Get(PipeTaskTemplateVariableReference taskTemplateVariable)
        => Get(taskTemplateVariable.TaskTemplate).Get(taskTemplateVariable.InputName);

    public bool Has(PipeTaskTemplateVariableReference taskTemplateVariable)
        => Get(taskTemplateVariable.TaskTemplate).Has(taskTemplateVariable.InputName);

    public PipeTemplateValues CopyMap()
    {
        var copy = new PipeTemplateValues();

        foreach (var (key, value) in _mapping)
        {
            copy._mapping.Add(key, value.CopyMap());
        }

        return copy;
    }
}
