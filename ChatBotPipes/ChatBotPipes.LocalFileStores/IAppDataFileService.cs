namespace ChatBotPipes.LocalFileStores;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAppDataFileService
{
    IEnumerable<string> GetDirectoryFileNames(string directoryPath);
    Task<string?> ReadFileAsync(string path);
    Task WriteFileAsync(string path, string? content);
}