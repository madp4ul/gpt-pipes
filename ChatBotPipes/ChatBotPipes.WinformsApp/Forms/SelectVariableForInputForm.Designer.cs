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
        ListViewGroup listViewGroup1 = new ListViewGroup("ListViewGroup fw f w", HorizontalAlignment.Left);
        ListViewGroup listViewGroup2 = new ListViewGroup("ListViewGroup", HorizontalAlignment.Left);
        ListViewItem listViewItem1 = new ListViewItem(new string[] { "Item1", "wef wwf" }, -1);
        ListViewItem listViewItem2 = new ListViewItem(new string[] { "Item2", "2Sub1", "2Sub2" }, -1);
        variablesListView = new ListView();
        SuspendLayout();
        // 
        // variablesListView
        // 
        variablesListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        listViewGroup1.Header = "ListViewGroup fw f w";
        listViewGroup1.Name = "listViewGroup1";
        listViewGroup2.Header = "ListViewGroup";
        listViewGroup2.Name = "listViewGroup2";
        variablesListView.Groups.AddRange(new ListViewGroup[] { listViewGroup1, listViewGroup2 });
        variablesListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        listViewItem1.Group = listViewGroup1;
        variablesListView.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2 });
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