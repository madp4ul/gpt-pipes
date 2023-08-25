namespace ChatBotPipes.WinformsApp.Controls;

partial class PipeTaskTemplateControl
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
        insertAboveButton = new Button();
        moveUpButton = new Button();
        moveDownButton = new Button();
        nameLabel = new Label();
        removeButton = new Button();
        SuspendLayout();
        // 
        // insertAboveButton
        // 
        insertAboveButton.Location = new Point(147, 3);
        insertAboveButton.Name = "insertAboveButton";
        insertAboveButton.Size = new Size(167, 23);
        insertAboveButton.TabIndex = 0;
        insertAboveButton.Text = "Insert Task Template above";
        insertAboveButton.UseVisualStyleBackColor = true;
        insertAboveButton.Click += InsertAboveButton_Click;
        // 
        // moveUpButton
        // 
        moveUpButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        moveUpButton.Location = new Point(320, 3);
        moveUpButton.Name = "moveUpButton";
        moveUpButton.Size = new Size(93, 23);
        moveUpButton.TabIndex = 1;
        moveUpButton.Text = "Move up";
        moveUpButton.UseVisualStyleBackColor = true;
        moveUpButton.Click += MoveUpButton_Click;
        // 
        // moveDownButton
        // 
        moveDownButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        moveDownButton.Location = new Point(320, 32);
        moveDownButton.Name = "moveDownButton";
        moveDownButton.Size = new Size(93, 23);
        moveDownButton.TabIndex = 2;
        moveDownButton.Text = "Move down";
        moveDownButton.UseVisualStyleBackColor = true;
        moveDownButton.Click += MoveDownButton_Click;
        // 
        // nameLabel
        // 
        nameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        nameLabel.Location = new Point(3, 36);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(311, 19);
        nameLabel.TabIndex = 3;
        nameLabel.Text = "Task template name";
        // 
        // removeButton
        // 
        removeButton.Location = new Point(3, 3);
        removeButton.Name = "removeButton";
        removeButton.Size = new Size(75, 23);
        removeButton.TabIndex = 4;
        removeButton.Text = "Remove";
        removeButton.UseVisualStyleBackColor = true;
        removeButton.Click += RemoveButton_Click;
        // 
        // PipeTaskTemplateControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(removeButton);
        Controls.Add(nameLabel);
        Controls.Add(moveDownButton);
        Controls.Add(moveUpButton);
        Controls.Add(insertAboveButton);
        Name = "PipeTaskTemplateControl";
        Size = new Size(416, 60);
        ResumeLayout(false);
    }

    #endregion

    private Button insertAboveButton;
    private Button moveUpButton;
    private Button moveDownButton;
    private Label nameLabel;
    private Button removeButton;
}
