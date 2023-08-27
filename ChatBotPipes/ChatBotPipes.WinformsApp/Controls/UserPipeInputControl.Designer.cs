namespace ChatBotPipes.WinformsApp.Controls;

partial class UserPipeInputControl
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
        userTaskInputControl = new UserTaskInputControl();
        SuspendLayout();
        // 
        // userTaskInputControl
        // 
        userTaskInputControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        userTaskInputControl.Location = new Point(3, 3);
        userTaskInputControl.Name = "userTaskInputControl";
        userTaskInputControl.Size = new Size(438, 229);
        userTaskInputControl.TabIndex = 0;
        userTaskInputControl.UserInputChanged += UserTaskInputControl_UserInputChanged;
        // 
        // UserPipeInputControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(userTaskInputControl);
        Name = "UserPipeInputControl";
        Size = new Size(444, 235);
        ResumeLayout(false);
    }

    #endregion

    private UserTaskInputControl userTaskInputControl;
}
