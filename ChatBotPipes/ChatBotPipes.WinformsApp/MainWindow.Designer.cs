namespace ChatBotPipes.WinformsApp;

partial class MainWindow
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        tabControl1 = new TabControl();
        pipePage = new TabPage();
        pipeManagementPage1 = new Pages.PipeManagementPage();
        taskTemplatePage = new TabPage();
        taskManagement1 = new Pages.TaskManagementPage();
        settingsPage = new TabPage();
        settingsPage1 = new Pages.SettingsPage();
        tabControl1.SuspendLayout();
        pipePage.SuspendLayout();
        taskTemplatePage.SuspendLayout();
        settingsPage.SuspendLayout();
        SuspendLayout();
        // 
        // tabControl1
        // 
        tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tabControl1.Controls.Add(pipePage);
        tabControl1.Controls.Add(taskTemplatePage);
        tabControl1.Controls.Add(settingsPage);
        tabControl1.Location = new Point(12, 12);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(892, 606);
        tabControl1.TabIndex = 0;
        // 
        // pipePage
        // 
        pipePage.Controls.Add(pipeManagementPage1);
        pipePage.Location = new Point(4, 24);
        pipePage.Name = "pipePage";
        pipePage.Padding = new Padding(3);
        pipePage.Size = new Size(884, 578);
        pipePage.TabIndex = 1;
        pipePage.Text = "Pipes";
        pipePage.UseVisualStyleBackColor = true;
        // 
        // pipeManagementPage1
        // 
        pipeManagementPage1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        pipeManagementPage1.Location = new Point(6, 6);
        pipeManagementPage1.Name = "pipeManagementPage1";
        pipeManagementPage1.Size = new Size(872, 566);
        pipeManagementPage1.TabIndex = 0;
        // 
        // taskTemplatePage
        // 
        taskTemplatePage.Controls.Add(taskManagement1);
        taskTemplatePage.Location = new Point(4, 24);
        taskTemplatePage.Name = "taskTemplatePage";
        taskTemplatePage.Size = new Size(884, 578);
        taskTemplatePage.TabIndex = 2;
        taskTemplatePage.Text = "Task Templates";
        taskTemplatePage.UseVisualStyleBackColor = true;
        // 
        // taskManagement1
        // 
        taskManagement1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        taskManagement1.CanEdit = true;
        taskManagement1.Location = new Point(3, 3);
        taskManagement1.Name = "taskManagement1";
        taskManagement1.Size = new Size(878, 572);
        taskManagement1.TabIndex = 0;
        // 
        // settingsPage
        // 
        settingsPage.Controls.Add(settingsPage1);
        settingsPage.Location = new Point(4, 24);
        settingsPage.Name = "settingsPage";
        settingsPage.Size = new Size(884, 578);
        settingsPage.TabIndex = 3;
        settingsPage.Text = "Settings";
        settingsPage.UseVisualStyleBackColor = true;
        // 
        // settingsPage1
        // 
        settingsPage1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        settingsPage1.Location = new Point(3, 3);
        settingsPage1.Name = "settingsPage1";
        settingsPage1.Size = new Size(878, 572);
        settingsPage1.TabIndex = 0;
        // 
        // MainWindow
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(916, 630);
        Controls.Add(tabControl1);
        Name = "MainWindow";
        Text = "ChatGPT Pipes";
        tabControl1.ResumeLayout(false);
        pipePage.ResumeLayout(false);
        taskTemplatePage.ResumeLayout(false);
        settingsPage.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TabControl tabControl1;
    private TabPage pipePage;
    private TabPage taskTemplatePage;
    private TabPage settingsPage;
    private Pages.SettingsPage settingsPage1;
    private Pages.TaskManagementPage taskManagement1;
    private Pages.PipeManagementPage pipeManagementPage1;
}
