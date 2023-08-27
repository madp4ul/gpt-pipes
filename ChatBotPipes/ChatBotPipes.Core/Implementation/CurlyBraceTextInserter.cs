namespace ChatBotPipes.Core.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public partial class CurlyBraceTextInserter : ITextInserter
{
    private const string _inputGroupName = "input";
    [GeneratedRegex("{{(?<input>\\w+?)}}")]
    private static partial Regex CurlyBracesRegex();

    public IEnumerable<string> GetInputs(string template)
    {
        var regex = CurlyBracesRegex();

        var inputs = regex.Matches(template)
            .Select(m => m.Groups[_inputGroupName])
            .Select(g => g.Value)
            .ToList();

        return inputs;
    }

    public string Insert(string template, TaskVariableValueMap toInsert)
    {
        var regex = CurlyBracesRegex();

        string result = regex.Replace(template, FindValueFromMapping);

        return result;

        string FindValueFromMapping(Match match)
        {
            string inputName = match.Groups[_inputGroupName].Value;

            string value = toInsert.Get(inputName);

            return value;
        }
    }
}
