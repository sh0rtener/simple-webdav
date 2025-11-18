using System.Linq.Expressions;
using System.Text.RegularExpressions;
using ShortenerNAS.WebApi.Common.Domain.Specifications;

namespace ShortenerNAS.WebApi.Folders.Validations;

public class IsValidFileSystemUnitName : Specification<string>
{
    public override Expression<Func<string, bool>> ToExpression()
    {
        return x => x.Length > 0 // && !Regex.IsMatch(x, "^[a-zA-Z0-9_\\-\\.]+$") 
                    &&
                    x.Length < 256;
    }
}