namespace ChatBotPipes.LocalFileStores;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserDataFileService
{
    Task<IEnumerable<string>> GetDirectoryFilePaths(string path);
    Task<string?> ReadFileAsync(string path);
    Task WriteFileAsync(string path, string? content);
}