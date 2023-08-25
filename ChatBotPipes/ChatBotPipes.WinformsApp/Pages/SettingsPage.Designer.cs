namespace ChatBotPipes.WinformsApp.Pages;

partial class SettingsPage
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
        label1 = new Label();
        apiKeyTextBox = new TextBox();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(16, 16);
        label1.Name = "label1";
        label1.Size = new Size(90, 15);
        label1.TabIndex = 0;
        label1.Text = "OpenAI Api Key";
        // 
        // apiKeyTextBox
        // 
        apiKeyTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        apiKeyTextBox.Location = new Point(112, 13);
        apiKeyTextBox.Name = "apiKeyTextBox";
        apiKeyTextBox.Size = new Size(363, 23);
        apiKeyTextBox.TabIndex = 1;
        apiKeyTextBox.Leave += apiKeyTextBox_Leave;
        // 
        // SettingsPage
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(apiKeyTextBox);
        Controls.Add(label1);
        Name = "SettingsPage";
        Size = new Size(478, 266);
        Load += SettingsPage_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private TextBox apiKeyTextBox;
}
