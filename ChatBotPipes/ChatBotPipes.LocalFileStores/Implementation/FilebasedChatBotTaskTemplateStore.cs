namespace ChatBotPipes.LocalFileStores.Implementation;

using ChatBotPipes.Client;
using ChatBotPipes.Core;
using ChatBotPipes.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class FilebasedChatBotTaskTemplateStore : IChatBotTaskTemplateStore
{
    private const string _taskTemplateDirectory = "taskTemplates";

    private readonly ICurrentUserService _currentUserService;
    private readonly IUserDataFileService _userDataFileService;

    public FilebasedChatBotTaskTemplateStore(ICurrentUserService currentUserService, IUserDataFileService userDataFileService)
    {
        _currentUserService = currentUserService;
        _userDataFileService = userDataFileService;
    }

    public async Task<List<ChatBotTaskTemplate>> GetTaskTemplatesAsync(User user)
    {
        await ThrowIfNotCurrentUserAsync(user);

        var fileNames = await _userDataFileService.GetDirectoryFilePaths(_taskTemplateDirectory);

        var result = new List<ChatBotTaskTemplate>();

        foreach (var fileName in fileNames)
        {
            var filePath = Path.Combine(_taskTemplateDirectory, fileName);

            var fileContent = await _userDataFileService.ReadFileAsync(filePath);

            if (fileContent is null)
            {
                continue;
            }

            var template = JsonSerializer.Deserialize<ChatBotTaskTemplate>(fileContent);

            if (template is null)
            {
                continue;
            }

            result.Add(template);
        }

        return result;
    }

    public async Task AddTaskTemplateAsync(User user, ChatBotTaskTemplate task)
    {
        await ThrowIfNotCurrentUserAsync(user);

        await UpdateTaskTemplateAsync(user, task);
    }

    public async Task RemoveTaskTemplateAsync(User user, ChatBotTaskTemplate task)
    {
        await ThrowIfNotCurrentUserAsync(user);

        var path = GetFilePath(task);

        await _userDataFileService.WriteFileAsync(path, null);
    }

    public async Task UpdateTaskTemplateAsync(User user, ChatBotTaskTemplate task)
    {
        await ThrowIfNotCurrentUserAsync(user);

        var path = GetFilePath(task);

        var fileContent = JsonSerializer.Serialize(task);

        await _userDataFileService.WriteFileAsync(path, fileContent);
    }

    private static string GetFilePath(ChatBotTaskTemplate task)
        => Path.Combine(_taskTemplateDirectory, $"task_{task.Id}");

    private async Task ThrowIfNotCurrentUserAsync(User user)
    {
        var currentUser = await _currentUserService.GetCurrentUserAsync();

        if (user != currentUser)
        {
            throw new InvalidOperationException("Can not interact with task templates of a user that is not current user.");
        }
    }
}
