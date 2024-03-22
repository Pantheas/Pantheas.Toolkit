namespace Pantheas.Toolkit.Core.Interfaces.Services;

public interface IFileSystemHelper
{
    string CacheDirectoryPath { get; }

    string AppDataDirectoryPath { get; }
}
