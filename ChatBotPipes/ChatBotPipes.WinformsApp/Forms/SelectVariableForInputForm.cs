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

public partial class SelectVariableForInputForm : Form
{
    private readonly Dictionary<ListViewGroup, MappedChatBotTaskTemplate?> _groupMap = new();

    public Dictionary<MappedChatBotTaskTemplate, List<string>>? Variables { get; private set; }

    public TaskTemplateVariableName? SelectedVariable { get; private set; }

    public SelectVariableForInputForm()
    {
        InitializeComponent();
    }

    public void SetValidVariables(ChatBotPipe pipe, Dictionary<MappedChatBotTaskTemplate, List<string>> variables)
    {
        Variables = variables;

        variablesListView.Items.Clear();
        _groupMap.Clear();

        AddUserInputItem();

        foreach (var (taskTemplate, variableList) in variables)
        {
            var group = new ListViewGroup(pipe.GetTaskNameInContext(taskTemplate));
            _groupMap.Add(group, taskTemplate);

            variablesListView.Groups.Add(group);

            var listItems = variableList
                .Select(v => new ListViewItem(v, group))
                .ToArray();

            variablesListView.Items.AddRange(listItems);
        }
    }

    private void AddUserInputItem()
    {
        var generalGroup = new ListViewGroup("General");
        variablesListView.Groups.Add(generalGroup);
        _groupMap.Add(generalGroup, null);

        var userInputItem = new ListViewItem("User input", generalGroup);
        variablesListView.Items.Add(userInputItem);
    }

    private void VariablesListView_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (variablesListView.SelectedItems.Count != 1)
        {
            return;
        }

        var selectedItem = variablesListView.SelectedItems[0];

        var group = selectedItem.Group;

        var taskTemplate = _groupMap[group];

        SelectedVariable = taskTemplate is null ? null : new TaskTemplateVariableName(taskTemplate, selectedItem.Text);

        DialogResult = DialogResult.OK;
        Close();
    }
}
