namespace ShortenerNAS.WebApi.Folders;

public enum ObjectAccess
{
    None = 0b0000_0000,
    Read = 0b0000_0001,
    ReadWrite = 0b0000_0110,
    ReadExecute = 0b0000_0101,
    ReadWriteExecute = 0b0000_0111,
}