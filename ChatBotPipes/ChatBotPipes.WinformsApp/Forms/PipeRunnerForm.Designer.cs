namespace ChatBotPipes.WinformsApp.Forms;

partial class PipeRunnerForm
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
        pipeNameLabel = new Label();
        outputPanel = new FlowLayoutPanel();
        label1 = new Label();
        runButton = new Button();
        userInputPanel = new FlowLayoutPanel();
        label2 = new Label();
        outputPanel.SuspendLayout();
        userInputPanel.SuspendLayout();
        SuspendLayout();
        // 
        // pipeNameLabel
        // 
        pipeNameLabel.AutoSize = true;
        pipeNameLabel.Location = new Point(15, 9);
        pipeNameLabel.Name = "pipeNameLabel";
        pipeNameLabel.Size = new Size(63, 15);
        pipeNameLabel.TabIndex = 1;
        pipeNameLabel.Text = "Pipe name";
        // 
        // outputPanel
        // 
        outputPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        outputPanel.AutoScroll = true;
        outputPanel.Controls.Add(label1);
        outputPanel.Location = new Point(567, 27);
        outputPanel.Name = "outputPanel";
        outputPanel.Size = new Size(437, 691);
        outputPanel.TabIndex = 3;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(3, 0);
        label1.Name = "label1";
        label1.Size = new Size(116, 15);
        label1.TabIndex = 0;
        label1.Text = "task output previews";
        // 
        // runButton
        // 
        runButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        runButton.Location = new Point(472, 695);
        runButton.Name = "runButton";
        runButton.Size = new Size(75, 23);
        runButton.TabIndex = 0;
        runButton.Text = "Run";
        runButton.UseVisualStyleBackColor = true;
        runButton.Click += RunButton_Click;
        // 
        // userInputPanel
        // 
        userInputPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        userInputPanel.AutoScroll = true;
        userInputPanel.Controls.Add(label2);
        userInputPanel.Location = new Point(12, 27);
        userInputPanel.Name = "userInputPanel";
        userInputPanel.Size = new Size(535, 662);
        userInputPanel.TabIndex = 5;
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
        // PipeRunnerForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1016, 730);
        Controls.Add(userInputPanel);
        Controls.Add(runButton);
        Controls.Add(outputPanel);
        Controls.Add(pipeNameLabel);
        Name = "PipeRunnerForm";
        Text = "PipeRunnerForm";
        outputPanel.ResumeLayout(false);
        outputPanel.PerformLayout();
        userInputPanel.ResumeLayout(false);
        userInputPanel.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private Label pipeNameLabel;
    private FlowLayoutPanel outputPanel;
    private Button runButton;
    private FlowLayoutPanel userInputPanel;
    private Label label1;
    private Label label2;
}