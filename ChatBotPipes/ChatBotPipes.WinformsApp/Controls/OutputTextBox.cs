namespace ChatBotPipes.WinformsApp.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class OutputTextBox : UserControl
{
    public string OutputText
    {
        get => textBox.Text;
        set
        {
            textBox.Text = ConvertNewLines(value);
        }
    }

    public OutputTextBox()
    {
        InitializeComponent();
    }

    private string ConvertNewLines(string text)
    {
        string withCorrectNewLines = NewLineRegex().Replace(text, Environment.NewLine);

        return withCorrectNewLines;
    }

    [GeneratedRegex("\n")]
    private static partial Regex NewLineRegex();
}
