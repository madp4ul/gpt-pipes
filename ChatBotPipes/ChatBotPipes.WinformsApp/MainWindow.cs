namespace ChatBotPipes.WinformsApp;

using ChatBotPipes.Client;
using ChatBotPipes.Core;
using ChatBotPipes.Server;

public partial class MainWindow : Form
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public async Task Example()
    {
        var chatBotPipeRunner = Services.Get<IChatBotPipeRunner>();
        var templateFiller = Services.Get<ITaskTemplateFiller>();
        var pipeStore = Services.Get<IChatBotPipeStore>();

        var userPipes = await pipeStore.GetPipesAsync(AppUser.Default);

        var newPipe = userPipes.First();

        var completions = chatBotPipeRunner.RunPipeAsync(newPipe, "Write unit test for...: ", templateFiller);

        IChatBotResponse? currentCompletion = null;

        await foreach (var completion in completions)
        {
            currentCompletion = completion;

            // todo append instructions to chat ui

            completion.DataReceived += Completion_ResponseReceived;

            await completion.AwaitCompletionAsync();

            completion.DataReceived -= Completion_ResponseReceived;
        }

        if (currentCompletion is not null)
        {
            string output = currentCompletion.GetCurrentResponse();
        }
    }

    private void Completion_ResponseReceived(string obj)
    {

    }
}
