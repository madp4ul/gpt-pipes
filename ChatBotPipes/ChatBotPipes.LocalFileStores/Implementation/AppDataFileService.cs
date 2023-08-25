namespace ChatBotPipes.LocalFileStores.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AppDataFileService : IAppDataFileService
{
    public async Task WriteFileAsync(string path, string? content)
    {
        string filePath = Path.Combine(GetApplicationPath(), path);

        EnsureDirectoryExists(filePath);

        if (content is null)
        {
            File.Delete(filePath);
        }
        else
        {
            await File.WriteAllTextAsync(filePath, content);
        }
    }

    public async Task<string?> ReadFileAsync(string path)
    {
        string filePath = Path.Combine(GetApplicationPath(), path);

        if (!File.Exists(filePath))
        {
            return null;
        }

        return await File.ReadAllTextAsync(filePath);
    }

    public IEnumerable<string> GetDirectoryFileNames(string path)
    {
        string directoryPath = Path.Combine(GetApplicationPath(), path);

        if (!Directory.Exists(directoryPath))
        {
            return Enumerable.Empty<string>();
        }

        var filePaths = Directory.GetFiles(directoryPath);

        return filePaths.Select(filePath => Path.GetFileName(filePath));
    }

    private static void EnsureDirectoryExists(string filePath)
    {
        string? directory = Path.GetDirectoryName(filePath);

        if (directory is not null && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    private static string GetApplicationPath()
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ChatBotPipes");
}
