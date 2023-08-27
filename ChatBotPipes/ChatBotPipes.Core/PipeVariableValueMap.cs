namespace ChatBotPipes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PipeVariableValueMap
{
    private readonly Dictionary<ChatBotTaskTemplate, TaskVariableValueMap> _mapping = new();

    public IEnumerable<TaskTemplateVariableName> MappedInputs => _mapping
        .SelectMany(kv => kv.Value.MappedInputs.Select(mi => new TaskTemplateVariableName(kv.Key, mi)))
        .ToList();

    public void AddInputValue(TaskTemplateVariableName templateVariable, string value)
    {
        var taskValues = GetTaskValues(templateVariable.TaskTemplate);

        taskValues.AddInputValue(templateVariable.InputName, value);
    }

    public void AddOutputValue(ChatBotTaskTemplate taskTemplate, string value)
    {
        var taskValues = GetTaskValues(taskTemplate);

        taskValues.AddOutputValue(value);
    }

    public TaskVariableValueMap Get(ChatBotTaskTemplate taskTemplate)
    {
        return GetTaskValues(taskTemplate);
    }

    private TaskVariableValueMap GetTaskValues(ChatBotTaskTemplate taskTemplate)
    {
        if (!_mapping.ContainsKey(taskTemplate))
        {
            _mapping[taskTemplate] = new TaskVariableValueMap();
        }

        return _mapping[taskTemplate];
    }

    public string Get(TaskTemplateVariableName taskTemplateVariable)
        => Get(taskTemplateVariable.TaskTemplate).Get(taskTemplateVariable.InputName);

    public PipeVariableValueMap CopyMap()
    {
        var copy = new PipeVariableValueMap();

        foreach (var (key, value) in _mapping)
        {
            copy._mapping.Add(key, value.CopyMap());
        }

        return copy;
    }
}
