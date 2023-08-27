namespace ChatBotPipes.WinformsApp.Forms;

partial class SelectVariableForInputForm
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
        variablesListView = new ListView();
        SuspendLayout();
        // 
        // variablesListView
        // 
        variablesListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        variablesListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        variablesListView.Location = new Point(12, 12);
        variablesListView.MultiSelect = false;
        variablesListView.Name = "variablesListView";
        variablesListView.Size = new Size(355, 230);
        variablesListView.TabIndex = 2;
        variablesListView.UseCompatibleStateImageBehavior = false;
        variablesListView.SelectedIndexChanged += VariablesListView_SelectedIndexChanged;
        // 
        // SelectVariableForInputForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(379, 254);
        Controls.Add(variablesListView);
        Name = "SelectVariableForInputForm";
        Text = "SelectVariableForInputForm";
        ResumeLayout(false);
    }

    #endregion
    private ListView variablesListView;
}