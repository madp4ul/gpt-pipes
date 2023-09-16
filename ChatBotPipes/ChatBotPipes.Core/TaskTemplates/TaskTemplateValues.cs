namespace ChatBotPipes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TaskTemplateValues
{
    public const string OutputKey = "output";

    private readonly Dictionary<string, string> _mapping = new();

    public IEnumerable<string> MappedInputs => _mapping.Keys;

    public void AddInputValue(string inputName, string value)
    {
        if (inputName == OutputKey)
        {
            throw new ArgumentException($"Can not add input with key {OutputKey}");
        }

        bool isDifferentValue = !_mapping.TryGetValue(inputName, out var existingInputValue) || existingInputValue != value;

        if (isDifferentValue)
        {
            AddValue(inputName, value);

            RemoveOutputValue(); // changing an input invalidates the output.
        }
    }

    public void AddOutputValue(string value)
    {
        AddValue(OutputKey, value);
    }

    public void RemoveOutputValue()
    {
        _mapping.Remove(OutputKey);
    }

    public void RemoveValue(string valueName)
    {
        _mapping.Remove(valueName);
        RemoveOutputValue();
    }

    private void AddValue(string inputName, string value)
    {
        if (_mapping.ContainsKey(inputName))
        {
            _mapping[inputName] = value;
        }
        else
        {
            _mapping.Add(inputName, value);
        }
    }

    public string Get(string key)
    {
        if (_mapping.TryGetValue(key, out string? mapping))
        {
            return mapping;
        }

        return "";
    }

    public bool Has(string key)
        => _mapping.ContainsKey(key);

    public bool HasOutput()
        => _mapping.ContainsKey(OutputKey);

    public TaskTemplateValues CopyMap()
    {
        var copy = new TaskTemplateValues();

        foreach (var (key, value) in _mapping)
        {
            copy._mapping.Add(key, value);
        }

        return copy;
    }
}
