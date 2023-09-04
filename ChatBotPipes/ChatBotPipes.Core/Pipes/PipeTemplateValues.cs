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

    public void SetInputValue(PipeTaskTemplateVariableReference templateVariable, string value)
    {
        var taskValues = GetTaskValues(templateVariable.ReferencedTaskTemplate);
        RemoveOutputValue(templateVariable.ReferencedTaskTemplate); // changing an input invalidates the output.

        taskValues.AddInputValue(templateVariable.ReferencedVariableName, value);

    }

    public void SetOutputValue(PipeTaskTemplateUsage taskTemplate, string value)
    {
        var taskValues = GetTaskValues(taskTemplate);

        taskValues.AddOutputValue(value);
    }

    public void RemoveOutputValue(PipeTaskTemplateUsage taskTemplate)
    {
        var values = GetTaskValues(taskTemplate);

        values.RemoveOutputValue();

        foreach (var otherTaskUsage in _mapping.Keys)
        {
            bool referencesRemovedOutputValue = otherTaskUsage.InputVariableReferences.Values
                .Any(ReferencesRemovedOutputValue);

            if (referencesRemovedOutputValue)
            {
                RemoveOutputValue(otherTaskUsage);
            }
        }

        bool ReferencesRemovedOutputValue(PipeTaskTemplateVariableReference variableReference)
            => variableReference.ReferencedVariableName == TaskTemplateValues.OutputKey
            && variableReference.ReferencedTaskTemplate == taskTemplate;
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
        => Get(taskTemplateVariable.ReferencedTaskTemplate).Get(taskTemplateVariable.ReferencedVariableName);

    public bool Has(PipeTaskTemplateVariableReference taskTemplateVariable)
        => Get(taskTemplateVariable.ReferencedTaskTemplate).Has(taskTemplateVariable.ReferencedVariableName);

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
