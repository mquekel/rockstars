using Rockstars.DataAccess.Models;

namespace Rockstars.DataAccess.Services.Import
{
    public interface IImportValidationRule<T>
    {
        ValidationResult IsValid(T item);
    }
}
