namespace ChatBotPipes.LocalFileStores.Implementation;

using ChatBotPipes.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserDataFileService : IUserDataFileService
{
    private readonly IAppDataFileService _appDataFileService;
    private readonly ICurrentUserService _currentUserService;

    public UserDataFileService(IAppDataFileService appDataFileService, ICurrentUserService currentUserService)
    {
        _appDataFileService = appDataFileService;
        _currentUserService = currentUserService;
    }

    public async Task WriteFileAsync(string path, string? content)
    {
        string filePath = Path.Combine(await GetUserPathAsync(), path);

        await _appDataFileService.WriteFileAsync(filePath, content);
    }

    public async Task<string?> ReadFileAsync(string path)
    {
        string filePath = Path.Combine(await GetUserPathAsync(), path);

        return await _appDataFileService.ReadFileAsync(filePath);
    }

    public async Task<IEnumerable<string>> GetDirectoryFilePaths(string path)
    {
        string directoryPath = Path.Combine(await GetUserPathAsync(), path);

        return _appDataFileService.GetDirectoryFileNames(directoryPath);
    }

    private async Task<string> GetUserPathAsync()
    {
        var user = await _currentUserService.GetCurrentUserAsync()
            ?? throw new InvalidOperationException("Can not write user data if there is no current user.");

        return Path.Combine("users", $"user_{user.Id}");
    }
}
