namespace ChatBotPipes.WinformsApp.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class OutputTextBox : UserControl
{
    [AllowNull]
    public override string Text
    {
        get => base.Text;
        set
        {
            base.Text = value;
            textBox.Text = WithWindowsStyleNewLines(value ?? "");
        }
    }

    public OutputTextBox()
    {
        InitializeComponent();
        textBox.Text = Text;
    }

    private string WithWindowsStyleNewLines(string text)
    {
        string withCorrectNewLines = NewLineRegex().Replace(text, Environment.NewLine);

        return withCorrectNewLines;
    }

    [GeneratedRegex(@"(?<!\r)\n")]
    private static partial Regex NewLineRegex();
}
