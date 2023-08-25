namespace ChatBotPipes.WinformsApp.Controls;

partial class ChatMessageControl
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
        messageTextBox = new TextBox();
        authorComboBox = new ComboBox();
        deleteButton = new Button();
        SuspendLayout();
        // 
        // messageTextBox
        // 
        messageTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        messageTextBox.Location = new Point(99, 3);
        messageTextBox.Multiline = true;
        messageTextBox.Name = "messageTextBox";
        messageTextBox.Size = new Size(201, 70);
        messageTextBox.TabIndex = 0;
        messageTextBox.Leave += MessageTextBox_Leave;
        // 
        // authorComboBox
        // 
        authorComboBox.FormattingEnabled = true;
        authorComboBox.Location = new Point(3, 3);
        authorComboBox.Name = "authorComboBox";
        authorComboBox.Size = new Size(90, 23);
        authorComboBox.TabIndex = 1;
        authorComboBox.SelectedIndexChanged += AuthorComboBox_SelectedIndexChanged;
        // 
        // deleteButton
        // 
        deleteButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        deleteButton.Location = new Point(306, 3);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new Size(24, 23);
        deleteButton.TabIndex = 2;
        deleteButton.Text = "-";
        deleteButton.UseVisualStyleBackColor = true;
        deleteButton.Click += DeleteButton_Click;
        // 
        // ChatMessageControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(deleteButton);
        Controls.Add(authorComboBox);
        Controls.Add(messageTextBox);
        Name = "ChatMessageControl";
        Size = new Size(333, 80);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox messageTextBox;
    private ComboBox authorComboBox;
    private Button deleteButton;
}
