namespace ChatBotPipes.WinformsApp.Controls;

partial class OutputTextBox
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
        textBox = new TextBox();
        SuspendLayout();
        // 
        // textBox
        // 
        textBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        textBox.Location = new Point(0, 0);
        textBox.Multiline = true;
        textBox.Name = "textBox";
        textBox.ReadOnly = true;
        textBox.ScrollBars = ScrollBars.Vertical;
        textBox.Size = new Size(275, 157);
        textBox.TabIndex = 0;
        // 
        // OutputTextBox
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(textBox);
        Name = "OutputTextBox";
        Size = new Size(275, 157);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox textBox;
}
