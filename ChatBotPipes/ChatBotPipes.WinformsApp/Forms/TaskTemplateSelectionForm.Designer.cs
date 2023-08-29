namespace ChatBotPipes.WinformsApp.Forms;

partial class TaskTemplateSelectionForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        taskManagementPage = new Pages.TaskManagementPage();
        takeSelectedTaskTemplateButton = new Button();
        SuspendLayout();
        // 
        // taskManagementPage
        // 
        taskManagementPage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        taskManagementPage.CanEdit = false;
        taskManagementPage.Location = new Point(12, 12);
        taskManagementPage.Name = "taskManagementPage";
        taskManagementPage.Size = new Size(812, 441);
        taskManagementPage.TabIndex = 0;
        taskManagementPage.SelectedTaskTemplateChanged += TaskManagementPage_SelectedTaskTemplateChanged;
        // 
        // takeSelectedTaskTemplateButton
        // 
        takeSelectedTaskTemplateButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        takeSelectedTaskTemplateButton.Enabled = false;
        takeSelectedTaskTemplateButton.Location = new Point(12, 468);
        takeSelectedTaskTemplateButton.Name = "takeSelectedTaskTemplateButton";
        takeSelectedTaskTemplateButton.Size = new Size(812, 23);
        takeSelectedTaskTemplateButton.TabIndex = 1;
        takeSelectedTaskTemplateButton.Text = "Take selected Task";
        takeSelectedTaskTemplateButton.UseVisualStyleBackColor = true;
        takeSelectedTaskTemplateButton.Click += TakeSelectedTaskTemplateButton_Click;
        // 
        // TaskTemplateSelectionForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(836, 503);
        Controls.Add(takeSelectedTaskTemplateButton);
        Controls.Add(taskManagementPage);
        Name = "TaskTemplateSelectionForm";
        Text = "TaskTemplateSelectionForm";
        ResumeLayout(false);
    }

    #endregion

    private Pages.TaskManagementPage taskManagementPage;
    private Button takeSelectedTaskTemplateButton;
}