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
        label1 = new Label();
        pipeNameLabel = new Label();
        inputTextBox = new TextBox();
        outputPanel = new FlowLayoutPanel();
        runButton = new Button();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 34);
        label1.Name = "label1";
        label1.Size = new Size(35, 15);
        label1.TabIndex = 0;
        label1.Text = "Input";
        // 
        // pipeNameLabel
        // 
        pipeNameLabel.AutoSize = true;
        pipeNameLabel.Location = new Point(53, 9);
        pipeNameLabel.Name = "pipeNameLabel";
        pipeNameLabel.Size = new Size(63, 15);
        pipeNameLabel.TabIndex = 1;
        pipeNameLabel.Text = "Pipe name";
        // 
        // inputTextBox
        // 
        inputTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        inputTextBox.Location = new Point(53, 31);
        inputTextBox.Multiline = true;
        inputTextBox.Name = "inputTextBox";
        inputTextBox.Size = new Size(735, 118);
        inputTextBox.TabIndex = 2;
        inputTextBox.Text = "Enter input here";
        // 
        // outputPanel
        // 
        outputPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        outputPanel.AutoScroll = true;
        outputPanel.Location = new Point(53, 184);
        outputPanel.Name = "outputPanel";
        outputPanel.Size = new Size(735, 254);
        outputPanel.TabIndex = 3;
        // 
        // runButton
        // 
        runButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        runButton.Location = new Point(713, 155);
        runButton.Name = "runButton";
        runButton.Size = new Size(75, 23);
        runButton.TabIndex = 4;
        runButton.Text = "Run";
        runButton.UseVisualStyleBackColor = true;
        runButton.Click += RunButton_Click;
        // 
        // PipeRunnerForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(runButton);
        Controls.Add(outputPanel);
        Controls.Add(inputTextBox);
        Controls.Add(pipeNameLabel);
        Controls.Add(label1);
        Name = "PipeRunnerForm";
        Text = "PipeRunnerForm";
        Load += PipeRunnerForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Label pipeNameLabel;
    private TextBox inputTextBox;
    private FlowLayoutPanel outputPanel;
    private Button runButton;
}