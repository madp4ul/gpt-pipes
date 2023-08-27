namespace ChatBotPipes.WinformsApp.Controls;

partial class PipeEditor
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
        nameTextBox = new TextBox();
        deletePipeButton = new Button();
        runPipeButton = new Button();
        addTaskToEndButton = new Button();
        taskTemplatePanel = new AutoTopDownLayoutControl();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(3, 12);
        label1.Name = "label1";
        label1.Size = new Size(39, 15);
        label1.TabIndex = 0;
        label1.Text = "Name";
        // 
        // nameTextBox
        // 
        nameTextBox.Location = new Point(47, 9);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new Size(197, 23);
        nameTextBox.TabIndex = 1;
        nameTextBox.Leave += NameTextBox_Leave;
        // 
        // deletePipeButton
        // 
        deletePipeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        deletePipeButton.Location = new Point(143, 358);
        deletePipeButton.Name = "deletePipeButton";
        deletePipeButton.Size = new Size(111, 23);
        deletePipeButton.TabIndex = 2;
        deletePipeButton.Text = "Delete Pipe";
        deletePipeButton.UseVisualStyleBackColor = true;
        deletePipeButton.Click += DeletePipeButton_Click;
        // 
        // runPipeButton
        // 
        runPipeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        runPipeButton.Location = new Point(393, 358);
        runPipeButton.Name = "runPipeButton";
        runPipeButton.Size = new Size(89, 23);
        runPipeButton.TabIndex = 3;
        runPipeButton.Text = "Run Pipe";
        runPipeButton.UseVisualStyleBackColor = true;
        runPipeButton.Click += RunPipeButton_Click;
        // 
        // addTaskToEndButton
        // 
        addTaskToEndButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        addTaskToEndButton.Location = new Point(3, 358);
        addTaskToEndButton.Name = "addTaskToEndButton";
        addTaskToEndButton.Size = new Size(134, 23);
        addTaskToEndButton.TabIndex = 5;
        addTaskToEndButton.Text = "Add Task To End";
        addTaskToEndButton.UseVisualStyleBackColor = true;
        addTaskToEndButton.Click += AddTaskToEndButton_Click;
        // 
        // taskTemplatePanel
        // 
        taskTemplatePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        taskTemplatePanel.Location = new Point(3, 38);
        taskTemplatePanel.Name = "taskTemplatePanel";
        taskTemplatePanel.Size = new Size(479, 314);
        taskTemplatePanel.TabIndex = 6;
        // 
        // PipeEditor
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(taskTemplatePanel);
        Controls.Add(addTaskToEndButton);
        Controls.Add(runPipeButton);
        Controls.Add(deletePipeButton);
        Controls.Add(nameTextBox);
        Controls.Add(label1);
        Name = "PipeEditor";
        Size = new Size(485, 384);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private TextBox nameTextBox;
    private Button deletePipeButton;
    private Button runPipeButton;
    private Button addTaskToEndButton;
    private AutoTopDownLayoutControl taskTemplatePanel;
}
