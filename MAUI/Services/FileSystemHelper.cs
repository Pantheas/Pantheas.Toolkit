using Pantheas.Toolkit.Core.Interfaces.Services;

namespace Pantheas.Toolkit.MAUI.Services;

public class FileSystemHelper :
    IFileSystemHelper
{
    public string CacheDirectoryPath =>
        FileSystem.Current.CacheDirectory;

    public string AppDataDirectoryPath =>
        FileSystem.Current.AppDataDirectory;
}
