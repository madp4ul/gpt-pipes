﻿namespace ChatBotPipes.WinformsApp.Controls;

partial class PipeRunnerTaskOutputControl
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
        components = new System.ComponentModel.Container();
        taskTemplateNameLabel = new Label();
        runButton = new Button();
        outputTextBox = new OutputTextBox();
        updateOutputTimer = new System.Windows.Forms.Timer(components);
        regenerateButton = new Button();
        SuspendLayout();
        // 
        // taskTemplateNameLabel
        // 
        taskTemplateNameLabel.AutoSize = true;
        taskTemplateNameLabel.Location = new Point(3, 10);
        taskTemplateNameLabel.Name = "taskTemplateNameLabel";
        taskTemplateNameLabel.Size = new Size(119, 15);
        taskTemplateNameLabel.TabIndex = 0;
        taskTemplateNameLabel.Text = "Task template name";
        // 
        // runButton
        // 
        runButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        runButton.Location = new Point(535, 34);
        runButton.Name = "runButton";
        runButton.Size = new Size(90, 40);
        runButton.TabIndex = 2;
        runButton.Text = "Run only this task";
        runButton.UseVisualStyleBackColor = true;
        runButton.Click += RunButton_Click;
        // 
        // outputTextBox
        // 
        outputTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        outputTextBox.Location = new Point(3, 34);
        outputTextBox.Name = "outputTextBox";
        outputTextBox.Size = new Size(526, 402);
        outputTextBox.TabIndex = 3;
        // 
        // updateOutputTimer
        // 
        updateOutputTimer.Interval = 16;
        updateOutputTimer.Tick += UpdateOutputTimer_Tick;
        // 
        // regenerateButton
        // 
        regenerateButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        regenerateButton.Enabled = false;
        regenerateButton.Location = new Point(535, 80);
        regenerateButton.Name = "regenerateButton";
        regenerateButton.Size = new Size(90, 42);
        regenerateButton.TabIndex = 4;
        regenerateButton.Text = "Regenerate response";
        regenerateButton.UseVisualStyleBackColor = true;
        regenerateButton.Click += RegenerateOutputButton_Click;
        // 
        // PipeRunnerTaskOutputControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(regenerateButton);
        Controls.Add(outputTextBox);
        Controls.Add(runButton);
        Controls.Add(taskTemplateNameLabel);
        Name = "PipeRunnerTaskOutputControl";
        Size = new Size(630, 439);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label taskTemplateNameLabel;
    private Button runButton;
    private OutputTextBox outputTextBox;
    private System.Windows.Forms.Timer updateOutputTimer;
    private Button regenerateButton;
}
