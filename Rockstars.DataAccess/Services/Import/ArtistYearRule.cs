using System.Collections.Generic;
using System.Linq;
using Rockstars.DataAccess.Models;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.Services.Import
{
    public class ArtistYearRule : IImportValidationRule<Artist>
    {
        private readonly ISongSearchService _songSearchService;

        private const int EndYear = 2016;

        public ArtistYearRule(ISongSearchService songSearchService)
        {
            _songSearchService = songSearchService;
        }

        public ValidationResult IsValid(Artist item)
        {
            var songs = this._songSearchService.SearchByArtist(item.Name).ToList();
            if (!songs.Any())
            {
                return new ValidationResult { Status = ValidationStatus.Failed, Message = $"No songs found for artist {item.Name}."};
            }

            if(!YearIsValid(songs))
            {
                return new ValidationResult { Status = ValidationStatus.Failed, Message = $"Artist {item.Name} has no songs from before {EndYear}." };
            }

            return new ValidationResult {Status = ValidationStatus.Succeeded};
        }

        private static bool YearIsValid(IEnumerable<Song> songs)
        {
            return songs.Any(song => song.Year < EndYear);
        }
    }
}
