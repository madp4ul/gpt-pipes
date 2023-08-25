namespace ChatBotPipes.Core.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public partial class CurlyBraceTextInserter : ITextInserter
{
    public string Insert(string template, string toInsert)
        => MyRegex().Replace(template, toInsert);

    [GeneratedRegex("{{input}}")]
    private static partial Regex MyRegex();
}
