namespace ChatBotPipes.LocalFileStores.Implementation;

using ChatBotPipes.Client;
using ChatBotPipes.Core;
using ChatBotPipes.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class FilebasedChatBotPipeStore : IChatBotPipeStore
{
    private const string _pipesDirectory = "pipes";

    private readonly ICurrentUserService _currentUserService;
    private readonly IUserDataFileService _userDataFileService;

    private readonly IChatBotTaskTemplateStore _chatBotTaskTemplateStore;

    public FilebasedChatBotPipeStore(ICurrentUserService currentUserService, IUserDataFileService userDataFileService, IChatBotTaskTemplateStore chatBotTaskTemplateStore)
    {
        _currentUserService = currentUserService;
        _userDataFileService = userDataFileService;
        _chatBotTaskTemplateStore = chatBotTaskTemplateStore;
    }

    public async Task<List<ChatBotPipe>> GetPipesAsync(User user)
    {
        await ThrowIfNotCurrentUserAsync(user);

        var allTaskTemplates = (await _chatBotTaskTemplateStore.GetTaskTemplatesAsync(user))
            .ToDictionary(tt => tt.Id, tt => tt);

        var fileNames = await _userDataFileService.GetDirectoryFilePaths(_pipesDirectory);

        var result = new List<ChatBotPipe>();

        foreach (var fileName in fileNames)
        {
            var filePath = Path.Combine(_pipesDirectory, fileName);

            var fileContent = await _userDataFileService.ReadFileAsync(filePath);

            if (fileContent is null)
            {
                continue;
            }

            var pipe = JsonSerializer.Deserialize<StoredPipe>(fileContent);

            if (pipe is null)
            {
                continue;
            }

            var pipeTaskTemplates = pipe.TaskTemplatesIds
                .Where(id => allTaskTemplates.ContainsKey(id.Id)) // filter out references to deleted templates.
                .Select(id => allTaskTemplates[id.Id])
                .ToList();

            result.Add(new ChatBotPipe(pipeTaskTemplates, pipe.Name, pipe.Id));
        }

        return result;
    }

    public async Task AddPipeAsync(User user, ChatBotPipe pipe)
    {
        await ThrowIfNotCurrentUserAsync(user);

        await UpdatePipeAsync(user, pipe);
    }

    public async Task RemovePipeAsync(User user, ChatBotPipe pipe)
    {
        await ThrowIfNotCurrentUserAsync(user);

        var path = GetFilePath(pipe);

        await _userDataFileService.WriteFileAsync(path, null);
    }

    public async Task UpdatePipeAsync(User user, ChatBotPipe pipe)
    {
        await ThrowIfNotCurrentUserAsync(user);

        var path = GetFilePath(pipe);

        var storedTaskTemplateReferences = pipe.Tasks
            .Select(t => new TaskTemplateReference(t.Id))
            .ToList();

        var storedPipe = new StoredPipe(pipe.Id, pipe.Name, storedTaskTemplateReferences);

        var fileContent = JsonSerializer.Serialize(storedPipe);

        await _userDataFileService.WriteFileAsync(path, fileContent);
    }

    private async Task ThrowIfNotCurrentUserAsync(User user)
    {
        var currentUser = await _currentUserService.GetCurrentUserAsync();

        if (user != currentUser)
        {
            throw new InvalidOperationException("Can not interact with task templates of a user that is not current user.");
        }
    }

    private static string GetFilePath(ChatBotPipe pipe)
        => Path.Combine(_pipesDirectory, $"pipe_{pipe.Id}");

    private record TaskTemplateReference(Guid Id);
    private record StoredPipe(Guid Id, string Name, List<TaskTemplateReference> TaskTemplatesIds);
}
