using ShortenerNAS.WebApi.Folders.Exceptions;
using ShortenerNAS.WebApi.Folders.Validations;

namespace ShortenerNAS.WebApi.Folders;

public class FileSystemUnitService
{
    public Folder GetFolderByPath(string path)
    {
        var folder = new Folder(path, new ObjectOwnersValue("-", "-", "-"),
            new ObjectAccessValue(ObjectAccess.None, ObjectAccess.None, ObjectAccess.None), null);
        GetFoldersByPath(path, folder);

        folder.SetAsParent();
        return folder;
    }

    private void GetFoldersByPath(string path, Folder folder)
    {
        if (!Directory.GetDirectories(path).Any() && !Directory.GetFiles(path).Any())
            return;

        if (!new IsValidFileSystemUnitName().IsSatisfiedBy(path))
            throw new InvalidFileSystemUnitNameException();

        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException();


        var directories = Directory.GetDirectories(path);
        foreach (var directory in directories)
        {
            var child = new Folder(directory.Split("/")[^1], new ObjectOwnersValue("-", "-", "-"),
                new ObjectAccessValue(ObjectAccess.None, ObjectAccess.None, ObjectAccess.None), null);
            GetFoldersByPath(directory, child);

            folder.AddFolder(child);
        }

        var files = Directory.GetFiles(path);
        foreach (var file in files)
        {
            var fileInfo = new FileInfo(file);
            var filename = fileInfo.Name;
            var extension = "";
            if (filename.LastIndexOf('.') != -1)
            {
                extension = filename.Substring(filename.LastIndexOf('.'));
                if (extension.Equals(fileInfo.Extension))
                    filename = filename.Remove(filename.LastIndexOf('.'));
                extension = extension.Replace(".", "");
            }
            var folderFile = new FolderFile(file, filename, new ObjectOwnersValue("-", "-", "-"),
                new ObjectAccessValue(ObjectAccess.None, ObjectAccess.None, ObjectAccess.None), folder,
                extension, FolderFile.IsExecutableFile(extension));
            folder.AddFile(folderFile);
        }
    }
}