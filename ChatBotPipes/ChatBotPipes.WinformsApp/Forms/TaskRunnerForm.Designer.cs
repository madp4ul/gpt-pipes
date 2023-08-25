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
        label2 = new Label();
        inputTextBox = new TextBox();
        runButton = new Button();
        label1 = new Label();
        outputTextBox = new Controls.OutputTextBox();
        SuspendLayout();
        // 
        // nameLabel
        // 
        nameLabel.AutoSize = true;
        nameLabel.Location = new Point(53, 9);
        nameLabel.Name = "nameLabel";
        nameLabel.Size = new Size(62, 15);
        nameLabel.TabIndex = 0;
        nameLabel.Text = "Task name";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(12, 30);
        label2.Name = "label2";
        label2.Size = new Size(35, 15);
        label2.TabIndex = 1;
        label2.Text = "Input";
        // 
        // inputTextBox
        // 
        inputTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        inputTextBox.Location = new Point(53, 27);
        inputTextBox.Multiline = true;
        inputTextBox.Name = "inputTextBox";
        inputTextBox.Size = new Size(658, 213);
        inputTextBox.TabIndex = 2;
        inputTextBox.Text = "enter input here";
        // 
        // runButton
        // 
        runButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        runButton.Location = new Point(636, 246);
        runButton.Name = "runButton";
        runButton.Size = new Size(75, 23);
        runButton.TabIndex = 4;
        runButton.Text = "Run";
        runButton.UseVisualStyleBackColor = true;
        runButton.Click += RunButton_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(2, 278);
        label1.Name = "label1";
        label1.Size = new Size(45, 15);
        label1.TabIndex = 6;
        label1.Text = "Output";
        // 
        // outputTextBox
        // 
        outputTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        outputTextBox.Location = new Point(53, 278);
        outputTextBox.Name = "outputTextBox";
        outputTextBox.OutputText = "";
        outputTextBox.Size = new Size(658, 160);
        outputTextBox.TabIndex = 7;
        // 
        // TaskRunnerForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(723, 450);
        Controls.Add(outputTextBox);
        Controls.Add(label1);
        Controls.Add(runButton);
        Controls.Add(inputTextBox);
        Controls.Add(label2);
        Controls.Add(nameLabel);
        Name = "TaskRunnerForm";
        Text = "TaskRunnerForm";
        Load += TaskRunnerForm_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label nameLabel;
    private Label label2;
    private TextBox inputTextBox;
    private Button runButton;
    private Label label1;
    private Controls.OutputTextBox outputTextBox;
}