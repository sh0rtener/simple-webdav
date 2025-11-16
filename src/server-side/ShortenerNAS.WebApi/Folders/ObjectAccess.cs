namespace ShortenerNAS.WebApi.Folders;

public enum ObjectAccess
{
    None = 0x000,
    Read = 0x100,
    ReadWrite = 0x110,
    ReadExecute = 0x101,
    ReadWriteExecute = 0x111,
}