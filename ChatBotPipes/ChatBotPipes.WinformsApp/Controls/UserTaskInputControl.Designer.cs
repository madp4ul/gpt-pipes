namespace ChatBotPipes.WinformsApp.Controls;

partial class UserTaskInputControl
{
    /// <summary> 
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Vom Komponenten-Designer generierter Code

    /// <summary> 
    /// Erforderliche Methode für die Designerunterstützung. 
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
        inputNameLabel = new Label();
        userInputTextBox = new TextBox();
        SuspendLayout();
        // 
        // inputNameLabel
        // 
        inputNameLabel.AutoSize = true;
        inputNameLabel.Location = new Point(3, 6);
        inputNameLabel.Name = "inputNameLabel";
        inputNameLabel.Size = new Size(68, 15);
        inputNameLabel.TabIndex = 0;
        inputNameLabel.Text = "Input name";
        inputNameLabel.TextAlign = ContentAlignment.TopRight;
        // 
        // userInputTextBox
        // 
        userInputTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        userInputTextBox.Location = new Point(3, 24);
        userInputTextBox.Multiline = true;
        userInputTextBox.Name = "userInputTextBox";
        userInputTextBox.ScrollBars = ScrollBars.Vertical;
        userInputTextBox.Size = new Size(502, 243);
        userInputTextBox.TabIndex = 1;
        userInputTextBox.TextChanged += TextBox1_TextChanged;
        // 
        // UserTaskInputControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(userInputTextBox);
        Controls.Add(inputNameLabel);
        Name = "UserTaskInputControl";
        Size = new Size(508, 270);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label inputNameLabel;
    private TextBox userInputTextBox;
}
