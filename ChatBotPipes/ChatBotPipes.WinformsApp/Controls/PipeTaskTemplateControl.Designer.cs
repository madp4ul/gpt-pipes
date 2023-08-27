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
        chatBotNameLabel = new Label();
        label1 = new Label();
        label2 = new Label();
        inputMappingPanel = new AutoTopDownLayoutControl();
        SuspendLayout();
        // 
        // insertAboveButton
        // 
        insertAboveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        insertAboveButton.Location = new Point(405, 3);
        insertAboveButton.Name = "insertAboveButton";
        insertAboveButton.Size = new Size(93, 23);
        insertAboveButton.TabIndex = 0;
        insertAboveButton.Text = "Insert above";
        insertAboveButton.UseVisualStyleBackColor = true;
        insertAboveButton.Click += InsertAboveButton_Click;
        // 
        // moveUpButton
        // 
        moveUpButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        moveUpButton.Location = new Point(405, 32);
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
        moveDownButton.Location = new Point(405, 61);
        moveDownButton.Name = "moveDownButton";
        moveDownButton.Size = new Size(93, 23);
        moveDownButton.TabIndex = 2;
        moveDownButton.Text = "Move down";
        moveDownButton.UseVisualStyleBackColor = true;
        moveDownButton.Click += MoveDownButton_Click;
        // 
        // nameLabel
        // 
        nameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        nameLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
        nameLabel.Location = new Point(50, 4);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(253, 22);
        nameLabel.TabIndex = 3;
        nameLabel.Text = "Task template name";
        // 
        // removeButton
        // 
        removeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        removeButton.Location = new Point(405, 131);
        removeButton.Name = "removeButton";
        removeButton.Size = new Size(93, 23);
        removeButton.TabIndex = 4;
        removeButton.Text = "Remove";
        removeButton.UseVisualStyleBackColor = true;
        removeButton.Click += RemoveButton_Click;
        // 
        // chatBotNameLabel
        // 
        chatBotNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        chatBotNameLabel.Location = new Point(280, 3);
        chatBotNameLabel.Name = "chatBotNameLabel";
        chatBotNameLabel.Size = new Size(119, 19);
        chatBotNameLabel.TabIndex = 5;
        chatBotNameLabel.Text = "Chat bot name";
        chatBotNameLabel.TextAlign = ContentAlignment.TopRight;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(3, 3);
        label1.Name = "label1";
        label1.Size = new Size(32, 15);
        label1.TabIndex = 7;
        label1.Text = "Task:";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(3, 32);
        label2.Name = "label2";
        label2.Size = new Size(43, 15);
        label2.TabIndex = 8;
        label2.Text = "Inputs:";
        // 
        // inputMappingPanel
        // 
        inputMappingPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        inputMappingPanel.BorderStyle = BorderStyle.Fixed3D;
        inputMappingPanel.Location = new Point(3, 50);
        inputMappingPanel.Name = "inputMappingPanel";
        inputMappingPanel.Size = new Size(396, 104);
        inputMappingPanel.TabIndex = 9;
        // 
        // PipeTaskTemplateControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BorderStyle = BorderStyle.FixedSingle;
        Controls.Add(inputMappingPanel);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(chatBotNameLabel);
        Controls.Add(removeButton);
        Controls.Add(nameLabel);
        Controls.Add(moveDownButton);
        Controls.Add(moveUpButton);
        Controls.Add(insertAboveButton);
        Name = "PipeTaskTemplateControl";
        Size = new Size(501, 157);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button insertAboveButton;
    private Button moveUpButton;
    private Button moveDownButton;
    private Label nameLabel;
    private Button removeButton;
    private Label chatBotNameLabel;
    private Label label1;
    private Label label2;
    private AutoTopDownLayoutControl inputMappingPanel;
}
