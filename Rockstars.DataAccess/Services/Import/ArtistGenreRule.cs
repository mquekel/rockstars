using System.Collections.Generic;
using System.Linq;
using Rockstars.DataAccess.Models;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.Services.Import
{
    public class ArtistGenreRule : IImportValidationRule<Artist>
    {
        private readonly ISongSearchService _songSearchService;

        private const string Genre = "Metal";

        public ArtistGenreRule(ISongSearchService songSearchService)
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

            if(!GenreIsValid(songs))
            {
                return new ValidationResult { Status = ValidationStatus.Failed, Message = $"Artist {item.Name} has no songs of genre {Genre}." };
            }
            return new ValidationResult {Status = ValidationStatus.Succeeded};
        }

        private static bool GenreIsValid(IEnumerable<Song> songs)
        {
            return songs.Any(song => song.Genre.ToLowerInvariant().Contains(Genre.ToLower()));
        }
    }
}
