namespace ChatBotPipes.WinformsApp.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class AutoTopDownLayoutControl : UserControl
{
    public IReadOnlyList<Control> Rows => tableLayoutPanel.Controls.Cast<Control>().ToList();

    public AutoTopDownLayoutControl()
    {
        InitializeComponent();

        int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
        tableLayoutPanel.Padding = new Padding(0, 0, vertScrollWidth, 0);

        tableLayoutPanel.RowStyles.Clear();

        tableLayoutPanel.RowCount = 0;
    }

    public void AddRow(Control control)
    {
        control.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
        //control.Width = this.Width - control.Margin.Left - control.Margin.Right;

        tableLayoutPanel.RowCount++;

        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        tableLayoutPanel.Controls.Add(control, 0, tableLayoutPanel.Controls.Count);
    }

    public void ClearRows()
    {
        tableLayoutPanel.Controls.Clear();

        tableLayoutPanel.RowStyles.Clear();

        tableLayoutPanel.RowCount = 0;
    }

    public void SetRowIndex(Control control, int rowIndex)
    {
        tableLayoutPanel.SetRow(control, rowIndex);
    }

    public void RemoveRow(Control control)
    {
        tableLayoutPanel.RowStyles.RemoveAt(tableLayoutPanel.RowStyles.Count - 1);
        tableLayoutPanel.Controls.Remove(control);

        tableLayoutPanel.RowCount--;
    }
}
