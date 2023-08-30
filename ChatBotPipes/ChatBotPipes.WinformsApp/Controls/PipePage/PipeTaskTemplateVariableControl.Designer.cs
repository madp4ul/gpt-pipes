namespace ChatBotPipes.WinformsApp.Controls;

partial class PipeTaskTemplateVariableControl
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
        inputNameLabel = new Label();
        variableReferenceLabel = new Label();
        updateVariableReferenceButton = new Button();
        SuspendLayout();
        // 
        // inputNameLabel
        // 
        inputNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        inputNameLabel.Location = new Point(0, 0);
        inputNameLabel.Name = "inputNameLabel";
        inputNameLabel.Size = new Size(95, 31);
        inputNameLabel.TabIndex = 0;
        inputNameLabel.Text = "Input name";
        inputNameLabel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // variableReferenceLabel
        // 
        variableReferenceLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        variableReferenceLabel.Location = new Point(101, 0);
        variableReferenceLabel.Name = "variableReferenceLabel";
        variableReferenceLabel.Size = new Size(226, 31);
        variableReferenceLabel.TabIndex = 1;
        variableReferenceLabel.Text = "Variable reference description";
        variableReferenceLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // updateVariableReferenceButton
        // 
        updateVariableReferenceButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        updateVariableReferenceButton.Location = new Point(333, 0);
        updateVariableReferenceButton.Name = "updateVariableReferenceButton";
        updateVariableReferenceButton.Size = new Size(58, 31);
        updateVariableReferenceButton.TabIndex = 2;
        updateVariableReferenceButton.Text = "Update";
        updateVariableReferenceButton.UseVisualStyleBackColor = true;
        updateVariableReferenceButton.Click += UpdateVariableReferenceButton_Click;
        // 
        // PipeInputMappingControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(updateVariableReferenceButton);
        Controls.Add(variableReferenceLabel);
        Controls.Add(inputNameLabel);
        Name = "PipeInputMappingControl";
        Size = new Size(391, 31);
        ResumeLayout(false);
    }

    #endregion

    private Label inputNameLabel;
    private Label variableReferenceLabel;
    private Button updateVariableReferenceButton;
}
