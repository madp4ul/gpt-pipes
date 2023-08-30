namespace ChatBotPipes.LocalFileStores.Implementation;

using ChatBotPipes.Client;
using ChatBotPipes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class LocalFileApiKeyStore : IApiKeyStore
{
    private const string _filePath = $"apiKey.txt";

    private readonly IUserDataFileService _userDataFileService;

    public LocalFileApiKeyStore(IUserDataFileService userDataFileService)
    {
        _userDataFileService = userDataFileService;
    }

    public async Task<ApiKey?> GetApiKeyAsync()
    {
        string? apiKeyValue = await _userDataFileService.ReadFileAsync(_filePath);

        return apiKeyValue is null ? null : new ApiKey(apiKeyValue);
    }

    public async Task SetApiKeyAsync(ApiKey? apiKey)
    {
        await _userDataFileService.WriteFileAsync(_filePath, apiKey?.Value);
    }
}
