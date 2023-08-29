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
        runButton = new Button();
        splitContainer = new SplitContainer();
        userInputPanel = new Controls.AutoTopDownLayoutControl();
        outputPanel = new Controls.AutoTopDownLayoutControl();
        cancelButton = new Button();
        ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
        splitContainer.Panel1.SuspendLayout();
        splitContainer.Panel2.SuspendLayout();
        splitContainer.SuspendLayout();
        SuspendLayout();
        // 
        // pipeNameLabel
        // 
        pipeNameLabel.AutoSize = true;
        pipeNameLabel.Location = new Point(3, 0);
        pipeNameLabel.Name = "pipeNameLabel";
        pipeNameLabel.Size = new Size(67, 15);
        pipeNameLabel.TabIndex = 1;
        pipeNameLabel.Text = "Pipe name";
        // 
        // runButton
        // 
        runButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        runButton.Location = new Point(874, 699);
        runButton.Name = "runButton";
        runButton.Size = new Size(101, 23);
        runButton.TabIndex = 0;
        runButton.Text = "Run";
        runButton.UseVisualStyleBackColor = true;
        runButton.Click += RunButton_Click;
        // 
        // splitContainer
        // 
        splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        splitContainer.Location = new Point(12, 12);
        splitContainer.Name = "splitContainer";
        // 
        // splitContainer.Panel1
        // 
        splitContainer.Panel1.BackColor = SystemColors.ControlLightLight;
        splitContainer.Panel1.Controls.Add(userInputPanel);
        splitContainer.Panel1.Controls.Add(pipeNameLabel);
        // 
        // splitContainer.Panel2
        // 
        splitContainer.Panel2.Controls.Add(outputPanel);
        splitContainer.Size = new Size(992, 681);
        splitContainer.SplitterDistance = 461;
        splitContainer.SplitterWidth = 30;
        splitContainer.TabIndex = 6;
        // 
        // userInputPanel
        // 
        userInputPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        userInputPanel.Location = new Point(3, 18);
        userInputPanel.Name = "userInputPanel";
        userInputPanel.Size = new Size(455, 660);
        userInputPanel.TabIndex = 6;
        // 
        // outputPanel
        // 
        outputPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        outputPanel.BackColor = SystemColors.ControlLightLight;
        outputPanel.Location = new Point(3, 4);
        outputPanel.Name = "outputPanel";
        outputPanel.Size = new Size(443, 674);
        outputPanel.TabIndex = 4;
        // 
        // cancelButton
        // 
        cancelButton.Enabled = false;
        cancelButton.Location = new Point(788, 699);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(80, 23);
        cancelButton.TabIndex = 7;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += CancelButton_Click;
        // 
        // PipeRunnerForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1016, 730);
        Controls.Add(cancelButton);
        Controls.Add(runButton);
        Controls.Add(splitContainer);
        Name = "PipeRunnerForm";
        Text = "PipeRunnerForm";
        splitContainer.Panel1.ResumeLayout(false);
        splitContainer.Panel1.PerformLayout();
        splitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
        splitContainer.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
    private Label pipeNameLabel;
    private Button runButton;
    private SplitContainer splitContainer;
    private Controls.AutoTopDownLayoutControl outputPanel;
    private Controls.AutoTopDownLayoutControl userInputPanel;
    private Button cancelButton;
}