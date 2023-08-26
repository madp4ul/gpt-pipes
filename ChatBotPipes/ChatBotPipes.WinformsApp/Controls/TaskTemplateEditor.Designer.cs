namespace ChatBotPipes.WinformsApp.Controls;

partial class TaskTemplateEditor
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
        taskTemplateNameTextBox = new TextBox();
        deleteTaskTemplateButton = new Button();
        addMessageButton = new Button();
        chatMessagePanel = new Panel();
        runButton = new Button();
        chatBotSelectionComboBox = new ComboBox();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(3, 11);
        label1.Name = "label1";
        label1.Size = new Size(39, 15);
        label1.TabIndex = 0;
        label1.Text = "Name";
        // 
        // taskTemplateNameTextBox
        // 
        taskTemplateNameTextBox.Location = new Point(48, 8);
        taskTemplateNameTextBox.Name = "taskTemplateNameTextBox";
        taskTemplateNameTextBox.Size = new Size(193, 23);
        taskTemplateNameTextBox.TabIndex = 1;
        taskTemplateNameTextBox.Leave += TaskTemplateNameTextBox_Leave;
        // 
        // deleteTaskTemplateButton
        // 
        deleteTaskTemplateButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        deleteTaskTemplateButton.Location = new Point(134, 335);
        deleteTaskTemplateButton.Name = "deleteTaskTemplateButton";
        deleteTaskTemplateButton.Size = new Size(152, 23);
        deleteTaskTemplateButton.TabIndex = 5;
        deleteTaskTemplateButton.Text = "Delete Task Template";
        deleteTaskTemplateButton.UseVisualStyleBackColor = true;
        deleteTaskTemplateButton.Click += DeleteTaskTemplateButton_Click;
        // 
        // addMessageButton
        // 
        addMessageButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        addMessageButton.Location = new Point(3, 335);
        addMessageButton.Name = "addMessageButton";
        addMessageButton.Size = new Size(125, 23);
        addMessageButton.TabIndex = 7;
        addMessageButton.Text = "Add Message";
        addMessageButton.UseVisualStyleBackColor = true;
        addMessageButton.Click += AddMessageButton_Click;
        // 
        // chatMessagePanel
        // 
        chatMessagePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        chatMessagePanel.AutoScroll = true;
        chatMessagePanel.Location = new Point(3, 37);
        chatMessagePanel.Margin = new Padding(0);
        chatMessagePanel.Name = "chatMessagePanel";
        chatMessagePanel.Size = new Size(518, 292);
        chatMessagePanel.TabIndex = 8;
        // 
        // runButton
        // 
        runButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        runButton.Location = new Point(446, 335);
        runButton.Name = "runButton";
        runButton.Size = new Size(75, 23);
        runButton.TabIndex = 9;
        runButton.Text = "Run";
        runButton.UseVisualStyleBackColor = true;
        runButton.Click += RunButton_Click;
        // 
        // chatBotSelectionComboBox
        // 
        chatBotSelectionComboBox.FormattingEnabled = true;
        chatBotSelectionComboBox.Location = new Point(247, 8);
        chatBotSelectionComboBox.Name = "chatBotSelectionComboBox";
        chatBotSelectionComboBox.Size = new Size(121, 23);
        chatBotSelectionComboBox.TabIndex = 10;
        chatBotSelectionComboBox.SelectedIndexChanged += ChatBotSelectionComboBox_SelectedIndexChanged;
        // 
        // TaskTemplateEditor
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(chatBotSelectionComboBox);
        Controls.Add(runButton);
        Controls.Add(chatMessagePanel);
        Controls.Add(addMessageButton);
        Controls.Add(deleteTaskTemplateButton);
        Controls.Add(taskTemplateNameTextBox);
        Controls.Add(label1);
        Name = "TaskTemplateEditor";
        Size = new Size(524, 361);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private TextBox taskTemplateNameTextBox;
    private Button deleteTaskTemplateButton;
    private Button addMessageButton;
    private Panel chatMessagePanel;
    private Button runButton;
    private ComboBox chatBotSelectionComboBox;
}
