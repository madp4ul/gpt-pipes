namespace ChatBotPipes.WinformsApp.Forms;

partial class TaskRunnerForm
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
        nameLabel = new Label();
        runButton = new Button();
        outputTextBox = new Controls.OutputTextBox();
        chatPreviewPanel = new FlowLayoutPanel();
        label1 = new Label();
        splitContainer1 = new SplitContainer();
        cancelButton = new Button();
        userInputPanel = new Controls.AutoTopDownLayoutControl();
        chatPreviewPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        SuspendLayout();
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Location = new Point(3, 0);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(68, 15);
        nameLabel.TabIndex = 0;
        nameLabel.Text = "Task name";
        // 
        // runButton
        // 
        runButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        runButton.Location = new Point(381, 651);
        runButton.Name = "runButton";
        runButton.Size = new Size(75, 23);
        runButton.TabIndex = 4;
        runButton.Text = "Run";
        runButton.UseVisualStyleBackColor = true;
        runButton.Click += RunButton_Click;
        // 
        // outputTextBox
        // 
        outputTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        outputTextBox.Location = new Point(6, 436);
        outputTextBox.Name = "outputTextBox";
        outputTextBox.OutputText = "";
        outputTextBox.Size = new Size(405, 238);
        outputTextBox.TabIndex = 7;
        // 
        // chatPreviewPanel
        // 
        chatPreviewPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        chatPreviewPanel.Controls.Add(label1);
        chatPreviewPanel.Location = new Point(3, 3);
        chatPreviewPanel.Name = "chatPreviewPanel";
        chatPreviewPanel.Size = new Size(408, 427);
        chatPreviewPanel.TabIndex = 8;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(3, 0);
        label1.Name = "label1";
        label1.Size = new Size(130, 15);
        label1.TabIndex = 0;
        label1.Text = "todo chat preview here";
        // 
        // splitContainer1
        // 
        splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        splitContainer1.Location = new Point(12, 12);
        splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.BackColor = SystemColors.ControlLightLight;
        splitContainer1.Panel1.Controls.Add(cancelButton);
        splitContainer1.Panel1.Controls.Add(userInputPanel);
        splitContainer1.Panel1.Controls.Add(runButton);
        splitContainer1.Panel1.Controls.Add(nameLabel);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.BackColor = SystemColors.ControlLightLight;
        splitContainer1.Panel2.Controls.Add(outputTextBox);
        splitContainer1.Panel2.Controls.Add(chatPreviewPanel);
        splitContainer1.Size = new Size(981, 677);
        splitContainer1.SplitterDistance = 459;
        splitContainer1.SplitterWidth = 30;
        splitContainer1.TabIndex = 10;
        // 
        // cancelButton
        // 
        cancelButton.Enabled = false;
        cancelButton.Location = new Point(300, 651);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(75, 23);
        cancelButton.TabIndex = 10;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += ButtonCancel_Click;
        // 
        // userInputPanel
        // 
        userInputPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        userInputPanel.Location = new Point(3, 18);
        userInputPanel.Name = "userInputPanel";
        userInputPanel.Size = new Size(453, 627);
        userInputPanel.TabIndex = 9;
        // 
        // TaskRunnerForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1005, 701);
        Controls.Add(splitContainer1);
        Name = "TaskRunnerForm";
        Text = "TaskRunnerForm";
        chatPreviewPanel.ResumeLayout(false);
        chatPreviewPanel.PerformLayout();
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel1.PerformLayout();
        splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Label nameLabel;
    private Button runButton;
    private Controls.OutputTextBox outputTextBox;
    private FlowLayoutPanel chatPreviewPanel;
    private Label label1;
    private SplitContainer splitContainer1;
    private Controls.AutoTopDownLayoutControl userInputPanel;
    private Button cancelButton;
}