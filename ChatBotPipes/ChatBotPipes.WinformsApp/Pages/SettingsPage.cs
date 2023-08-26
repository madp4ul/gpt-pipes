namespace ChatBotPipes.WinformsApp.Pages;

using ChatBotPipes.Client;
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

public partial class SettingsPage : UserControl
{
    private IApiKeyStore _apiKeyStore = null!;

    public SettingsPage()
    {
        InitializeComponent();
    }

    private async void SettingsPage_Load(object sender, EventArgs e)
    {
        if (DesignMode)
        {
            return;
        }

        InitializeServices();

        apiKeyTextBox.Text = (await _apiKeyStore.GetApiKeyAsync())?.Value ?? "";
    }

    private void InitializeServices()
    {
        _apiKeyStore = Services.Get<IApiKeyStore>();
    }

    private async void apiKeyTextBox_Leave(object sender, EventArgs e)
    {
        var apiKey = new ApiKey(apiKeyTextBox.Text);

        await _apiKeyStore.SetApiKeyAsync(apiKey);
    }

    private void buttonShowApiKey_Click(object sender, EventArgs e)
    {
        apiKeyTextBox.UseSystemPasswordChar = !apiKeyTextBox.UseSystemPasswordChar;
        buttonShowApiKey.Text = apiKeyTextBox.UseSystemPasswordChar ? "Show" : "Hide";
    }
}
