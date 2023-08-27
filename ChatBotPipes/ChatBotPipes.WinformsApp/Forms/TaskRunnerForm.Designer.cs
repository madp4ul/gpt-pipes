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
        userInputPanel = new FlowLayoutPanel();
        label2 = new Label();
        panel1 = new Panel();
        chatPreviewPanel = new FlowLayoutPanel();
        label1 = new Label();
        userInputPanel.SuspendLayout();
        panel1.SuspendLayout();
        chatPreviewPanel.SuspendLayout();
        SuspendLayout();
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Location = new Point(12, 9);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(62, 15);
        nameLabel.TabIndex = 0;
        nameLabel.Text = "Task name";
        // 
        // runButton
        // 
        runButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        runButton.Location = new Point(406, 666);
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
        outputTextBox.Location = new Point(3, 362);
        outputTextBox.Name = "outputTextBox";
        outputTextBox.OutputText = "";
        outputTextBox.Size = new Size(500, 297);
        outputTextBox.TabIndex = 7;
        // 
        // userInputPanel
        // 
        userInputPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        userInputPanel.AutoScroll = true;
        userInputPanel.Controls.Add(label2);
        userInputPanel.Location = new Point(12, 27);
        userInputPanel.Name = "userInputPanel";
        userInputPanel.Size = new Size(469, 630);
        userInputPanel.TabIndex = 8;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(3, 0);
        label2.Name = "label2";
        label2.Size = new Size(89, 15);
        label2.TabIndex = 0;
        label2.Text = "Input textboxes";
        // 
        // panel1
        // 
        panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        panel1.Controls.Add(chatPreviewPanel);
        panel1.Controls.Add(outputTextBox);
        panel1.Location = new Point(487, 27);
        panel1.Name = "panel1";
        panel1.Size = new Size(506, 662);
        panel1.TabIndex = 9;
        // 
        // chatPreviewPanel
        // 
        chatPreviewPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        chatPreviewPanel.Controls.Add(label1);
        chatPreviewPanel.Location = new Point(3, 3);
        chatPreviewPanel.Name = "chatPreviewPanel";
        chatPreviewPanel.Size = new Size(500, 353);
        chatPreviewPanel.TabIndex = 8;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(3, 0);
        label1.Name = "label1";
        label1.Size = new Size(128, 15);
        label1.TabIndex = 0;
        label1.Text = "todo chat preview here";
        // 
        // TaskRunnerForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1005, 701);
        Controls.Add(panel1);
        Controls.Add(userInputPanel);
        Controls.Add(runButton);
        Controls.Add(nameLabel);
        Name = "TaskRunnerForm";
        Text = "TaskRunnerForm";
        userInputPanel.ResumeLayout(false);
        userInputPanel.PerformLayout();
        panel1.ResumeLayout(false);
        chatPreviewPanel.ResumeLayout(false);
        chatPreviewPanel.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label nameLabel;
    private Button runButton;
    private Controls.OutputTextBox outputTextBox;
    private FlowLayoutPanel userInputPanel;
    private Panel panel1;
    private FlowLayoutPanel chatPreviewPanel;
    private Label label1;
    private Label label2;
}