namespace ChatBotPipes.WinformsApp.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class UserTaskInputControl : UserControl
{
    public event EventHandler<InputChange>? UserInputChanged;

    public string? UserInputName { get; private set; }

    public UserTaskInputControl()
    {
        InitializeComponent();
    }

    public void SetUserInputName(string name)
    {
        UserInputName = name;

        inputNameLabel.Text = name;
    }

    private void TextBox1_TextChanged(object sender, EventArgs e)
    {
        ArgumentNullException.ThrowIfNull(UserInputName);

        UserInputChanged?.Invoke(this, new InputChange(UserInputName, userInputTextBox.Text));
    }

    public record InputChange(string InputName, string UserInputValue);
}
