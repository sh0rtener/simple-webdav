using ShortenerNAS.WebApi.Common.Domain;

namespace ShortenerNAS.WebApi.Folders.Exceptions;

public class InvalidFileSystemUnitNameException() : DomainException("Неверное значение имени файла");