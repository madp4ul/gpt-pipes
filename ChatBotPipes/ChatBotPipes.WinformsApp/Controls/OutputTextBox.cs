namespace ChatBotPipes.WinformsApp.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            ScrollToEnd();
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

    public void ScrollToEnd()
    {
        textBox.Select(textBox.Text.Length, 0);
        textBox.ScrollToCaret();
    }

    [GeneratedRegex(@"(?<!\r)\n")]
    private static partial Regex NewLineRegex();
}
