namespace ChatBotPipes.WinformsApp.Pages;

partial class TaskManagementPage
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
        addTaskTemplateButton = new Button();
        panel1 = new Panel();
        taskTemplateListBox = new ListBox();
        taskTemplateEditor = new Controls.TaskTemplateEditor();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // addTaskTemplateButton
        // 
        addTaskTemplateButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        addTaskTemplateButton.Location = new Point(3, 482);
        addTaskTemplateButton.Name = "addTaskTemplateButton";
        addTaskTemplateButton.Size = new Size(194, 23);
        addTaskTemplateButton.TabIndex = 1;
        addTaskTemplateButton.Text = "Add Task Template";
        addTaskTemplateButton.UseVisualStyleBackColor = true;
        addTaskTemplateButton.Click += AddTaskTemplateButton_Click;
        // 
        // panel1
        // 
        panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        panel1.Controls.Add(taskTemplateListBox);
        panel1.Controls.Add(addTaskTemplateButton);
        panel1.Location = new Point(3, 3);
        panel1.Name = "panel1";
        panel1.Size = new Size(200, 508);
        panel1.TabIndex = 2;
        // 
        // taskTemplateListBox
        // 
        taskTemplateListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        taskTemplateListBox.FormattingEnabled = true;
        taskTemplateListBox.ItemHeight = 15;
        taskTemplateListBox.Location = new Point(3, 3);
        taskTemplateListBox.Name = "taskTemplateListBox";
        taskTemplateListBox.Size = new Size(194, 469);
        taskTemplateListBox.TabIndex = 2;
        taskTemplateListBox.SelectedIndexChanged += TaskTemplateListBox_SelectedIndexChanged;
        // 
        // taskTemplateEditor
        // 
        taskTemplateEditor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        taskTemplateEditor.CanEdit = true;
        taskTemplateEditor.Location = new Point(206, 3);
        taskTemplateEditor.Name = "taskTemplateEditor";
        taskTemplateEditor.Size = new Size(539, 508);
        taskTemplateEditor.TabIndex = 3;
        taskTemplateEditor.Visible = false;
        taskTemplateEditor.TaskTemplateUpdated += TaskTemplateEditor_TaskTemplateUpdated;
        taskTemplateEditor.TaskTemplateDeleted += TaskTemplateEditor_TaskTemplateDeleted;
        // 
        // TaskManagementPage
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(taskTemplateEditor);
        Controls.Add(panel1);
        Name = "TaskManagementPage";
        Size = new Size(748, 514);
        Load += TaskManagement_Load;
        panel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
    private Button addTaskTemplateButton;
    private Panel panel1;
    private Controls.TaskTemplateEditor taskTemplateEditor;
    private ListBox taskTemplateListBox;
}
