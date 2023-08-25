namespace ChatBotPipes.WinformsApp.Pages;

partial class PipeManagementPage
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
        pipesListBox = new ListBox();
        addPipeButton = new Button();
        pipeEditor = new Controls.PipeEditor();
        SuspendLayout();
        // 
        // pipesListBox
        // 
        pipesListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        pipesListBox.FormattingEnabled = true;
        pipesListBox.ItemHeight = 15;
        pipesListBox.Location = new Point(3, 3);
        pipesListBox.Name = "pipesListBox";
        pipesListBox.Size = new Size(194, 364);
        pipesListBox.TabIndex = 0;
        pipesListBox.SelectedIndexChanged += PipesListBox_SelectedIndexChanged;
        // 
        // addPipeButton
        // 
        addPipeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        addPipeButton.Location = new Point(3, 373);
        addPipeButton.Name = "addPipeButton";
        addPipeButton.Size = new Size(194, 23);
        addPipeButton.TabIndex = 1;
        addPipeButton.Text = "Add Pipe";
        addPipeButton.UseVisualStyleBackColor = true;
        addPipeButton.Click += AddPipeButton_Click;
        // 
        // pipeEditor
        // 
        pipeEditor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        pipeEditor.Location = new Point(203, 3);
        pipeEditor.Name = "pipeEditor";
        pipeEditor.Size = new Size(470, 396);
        pipeEditor.TabIndex = 2;
        pipeEditor.Visible = false;
        pipeEditor.PipeUpdated += PipeEditor_PipeUpdated;
        pipeEditor.PipeDeleted += PipeEditor_PipeDeleted;
        // 
        // PipeManagementPage
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(pipeEditor);
        Controls.Add(addPipeButton);
        Controls.Add(pipesListBox);
        Name = "PipeManagementPage";
        Size = new Size(676, 401);
        Load += PipeManagementPage_Load;
        ResumeLayout(false);
    }

    #endregion

    private ListBox pipesListBox;
    private Button addPipeButton;
    private Controls.PipeEditor pipeEditor;
}
