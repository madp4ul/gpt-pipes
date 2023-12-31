﻿namespace ChatBotPipes.Core.TaskTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITextInserter
{
    IEnumerable<string> GetInputs(string template);

    string Insert(string template, TaskTemplateValues toInsert);
}
