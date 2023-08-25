namespace ChatBotPipes.WinformsApp.Forms;

using ChatBotPipes.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class TaskTemplateSelectionForm : Form
{
    public ChatBotTaskTemplate? SelectedTaskTemplate { get; private set; }

    public TaskTemplateSelectionForm()
    {
        InitializeComponent();
    }

    private void TakeSelectedTaskTemplateButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;

        Close();
    }

    private void TaskManagementPage_SelectedTaskTemplateChanged(object sender, ChatBotTaskTemplate? selection)
    {
        takeSelectedTaskTemplateButton.Enabled = selection is not null;

        SelectedTaskTemplate = selection;
    }
}
