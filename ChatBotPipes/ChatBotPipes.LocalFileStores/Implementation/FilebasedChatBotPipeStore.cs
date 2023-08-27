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

            result.Add(FromStored(pipe, allTaskTemplates));
        }

        return result;
    }

    private ChatBotPipe FromStored(StoredPipe storedPipe, Dictionary<Guid, ChatBotTaskTemplate> allTaskTemplates)
    {
        var tasks = storedPipe.Tasks
            .Select(FromStored)
            .OfType<MappedChatBotTaskTemplate>()
            .ToList();

        return new ChatBotPipe(tasks, storedPipe.Name, storedPipe.Id);

        MappedChatBotTaskTemplate? FromStored(StoredMappedChatBotTaskTemplate storedTaskTemplate)
        {
            if (!allTaskTemplates.TryGetValue(storedTaskTemplate.TaskTemplate.Id, out ChatBotTaskTemplate? taskTemplate))
            {
                return null;
            }

            var inputMappings = storedTaskTemplate.InputMapping
                .Where(kv => allTaskTemplates.ContainsKey(kv.Value.TaskTemplate.Id))
                .ToDictionary(kv => kv.Key, kv => new TaskTemplateVariableName(allTaskTemplates[kv.Value.TaskTemplate.Id], kv.Value.InputName));

            return new MappedChatBotTaskTemplate(taskTemplate, inputMappings);
        }
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

        var storedMappedTaskTemplateReferences = pipe.Tasks
            .Select(ToStoredTask)
            .ToList();

        var storedPipe = new StoredPipe(pipe.Id, pipe.Name, storedMappedTaskTemplateReferences);

        var fileContent = JsonSerializer.Serialize(storedPipe);

        await _userDataFileService.WriteFileAsync(path, fileContent);

        static StoredMappedChatBotTaskTemplate ToStoredTask(MappedChatBotTaskTemplate mappedTaskTemplate)
        {
            var reference = new TaskTemplateReference(mappedTaskTemplate.TaskTemplate.Id);
            var inputMapping = mappedTaskTemplate.InputMapping
                .ToDictionary(kv => kv.Key, kv => ToStoredTaskTemplateVariableName(kv.Value));

            return new StoredMappedChatBotTaskTemplate(reference, inputMapping);

            static StoredTaskTemplateVariableName ToStoredTaskTemplateVariableName(TaskTemplateVariableName variableName)
                => new(new TaskTemplateReference(variableName.TaskTemplate.Id), variableName.InputName);
        }
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

    // have one counter part to each class that participates in defining the pipe.
    // The difference here is, that instead of references to the real task templates, it only stored the guid.

    private record StoredPipe(Guid Id, string Name, List<StoredMappedChatBotTaskTemplate> Tasks);

    private record StoredMappedChatBotTaskTemplate(TaskTemplateReference TaskTemplate, Dictionary<string, StoredTaskTemplateVariableName> InputMapping);

    private record StoredTaskTemplateVariableName(TaskTemplateReference TaskTemplate, string InputName);

    private record TaskTemplateReference(Guid Id);
}
