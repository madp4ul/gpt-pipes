namespace ChatBotPipes.LocalFileStores.Implementation;

using ChatBotPipes.Client;
using ChatBotPipes.Core;
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
        var tasks = new List<MappedChatBotTaskTemplate>();

        foreach (var storedTask in storedPipe.Tasks)
        {
            if (!allTaskTemplates.TryGetValue(storedTask.TaskTemplate.Id, out ChatBotTaskTemplate? taskTemplate))
            {
                continue; // the referenced task does not exist anymore.
            }

            var inputMappings = storedTask.InputMapping
                .Where(kv => allTaskTemplates.ContainsKey(kv.Value.TaskTemplate.Id))
                .Select(kv => (kv.Key, Value: GetVariableReference(kv.Value)))
                .Where(kv => kv.Value is not null)
                .ToDictionary(kv => kv.Key, kv => kv.Value!);

            tasks.Add(new MappedChatBotTaskTemplate(taskTemplate, inputMappings));
        }

        return new ChatBotPipe(tasks, storedPipe.Name, storedPipe.Id);

        TaskTemplateVariableName? GetVariableReference(StoredTaskTemplateVariableName storedVariableReference)
        {
            var mappedTaskTemplate = tasks[storedVariableReference.TaskTemplate.PipeIndex];

            if (mappedTaskTemplate.TaskTemplate.Id != storedVariableReference.TaskTemplate.Id)
            {
                return null; // At saved index we found a different task template than expected. Ignore this reference
            }

            return new TaskTemplateVariableName(mappedTaskTemplate, storedVariableReference.InputName);
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

        var storedMappedTaskTemplateReferences = new List<StoredMappedChatBotTaskTemplate>();

        foreach (var task in pipe.Tasks)
        {
            var reference = new TaskTemplateReference(task.TaskTemplate.Id);
            var inputMapping = task.InputMapping
                .ToDictionary(kv => kv.Key, kv => ToStoredTaskTemplateVariableName(kv.Value));

            storedMappedTaskTemplateReferences.Add(new StoredMappedChatBotTaskTemplate(reference, inputMapping));

            StoredTaskTemplateVariableName ToStoredTaskTemplateVariableName(TaskTemplateVariableName variableName)
            {
                int index = pipe.Tasks.IndexOf(variableName.TaskTemplate);
                return new StoredTaskTemplateVariableName(new MappedTaskTemplateReference(variableName.TaskTemplate.TaskTemplate.Id, index), variableName.InputName);
            }
        }

        var storedPipe = new StoredPipe(pipe.Id, pipe.Name, storedMappedTaskTemplateReferences);

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

    // have one counter part to each class that participates in defining the pipe.
    // The difference here is, that instead of references to the real task templates, it only stored the guid.

    private record StoredPipe(Guid Id, string Name, List<StoredMappedChatBotTaskTemplate> Tasks);

    private record StoredMappedChatBotTaskTemplate(TaskTemplateReference TaskTemplate, Dictionary<string, StoredTaskTemplateVariableName> InputMapping);

    private record StoredTaskTemplateVariableName(MappedTaskTemplateReference TaskTemplate, string InputName);

    private record MappedTaskTemplateReference(Guid Id, int PipeIndex);
    private record TaskTemplateReference(Guid Id);
}
