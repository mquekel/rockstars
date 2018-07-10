using System.Collections.Generic;
using System.Linq;
using Rockstars.DataAccess.Models;
using Rockstars.DataAccess.Repositories;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.Services.Import
{
    public class ArtistImportService : IArtistImportService
    {
        private readonly IEnumerable<IImportValidationRule<Artist>> _validationRules;

        private readonly IRepository<Artist> _artistRepository;

        public ArtistImportService(IEnumerable<IImportValidationRule<Artist>> validationRules, IRepository<Artist> artistRepository)
        {
            _artistRepository = artistRepository;
            _validationRules = validationRules;
        }

        public List<ValidationResult> Import(IEnumerable<Artist> artists)
        {
            var validationResults = new List<ValidationResult>();
            var artistToCreate = new List<Artist>();

            foreach (var artist in artists)
            {
                var results = this._validationRules.Select(rule => rule.IsValid(artist)).ToList();
                if (AllRulesAreValid(results))
                {
                    artistToCreate.Add(artist);
                }
                else
                {
                    validationResults.Add(results.FirstOrDefault());
                }
            }

            if (artistToCreate.Any())
            {
                _artistRepository.Create(artistToCreate);
            }

            return validationResults;
        }

        private static bool AllRulesAreValid(IEnumerable<ValidationResult> validationResults)
        {
            return validationResults.All(result => result.Status == ValidationStatus.Succeeded);
        }
    }
}
