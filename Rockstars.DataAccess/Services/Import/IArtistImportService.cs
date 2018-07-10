using System.Collections.Generic;
using Rockstars.DataAccess.Models;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.Services.Import
{
    public interface IArtistImportService
    {
        List<ValidationResult> Import(IEnumerable<Artist> artists);
    }
}