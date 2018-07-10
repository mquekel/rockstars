using System;
using System.Linq;
using Rockstars.DataAccess.Models;
using Rockstars.DataAccess.Repositories;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.Services.Import
{
    public class ArtistAlreadyExistRule : IImportValidationRule<Artist>
    {
        private readonly IRepository<Artist> _artistRepository;

        public ArtistAlreadyExistRule(IRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public ValidationResult IsValid(Artist item)
        {
            var artists = this._artistRepository.Search(artist =>
                artist.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase));
            if (artists.Any())
            {
                return new ValidationResult() {Status = ValidationStatus.Failed, Message = $"artist with name {item.Name} already exists."};
            }
            return new ValidationResult { Status = ValidationStatus.Succeeded};
        }

    }
}
