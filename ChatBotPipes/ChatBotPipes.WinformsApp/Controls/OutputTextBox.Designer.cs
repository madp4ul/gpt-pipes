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
        label1 = new Label();
        SuspendLayout();
        // 
        // textBox
        // 
        textBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        textBox.Location = new Point(51, 0);
        textBox.Multiline = true;
        textBox.Name = "textBox";
        textBox.ReadOnly = true;
        textBox.ScrollBars = ScrollBars.Vertical;
        textBox.Size = new Size(393, 363);
        textBox.TabIndex = 0;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(0, 3);
        label1.Name = "label1";
        label1.Size = new Size(43, 15);
        label1.TabIndex = 1;
        label1.Text = "Output";
        // 
        // OutputTextBox
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(label1);
        Controls.Add(textBox);
        Name = "OutputTextBox";
        Size = new Size(444, 363);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox textBox;
    private Label label1;
}
