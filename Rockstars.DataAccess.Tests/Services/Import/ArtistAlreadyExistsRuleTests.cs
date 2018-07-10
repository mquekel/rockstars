using System;
using System.Linq;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rockstars.DataAccess.Models;
using Rockstars.DataAccess.Repositories;
using Rockstars.DataAccess.Services.Import;
using Rockstars.Domain.Entities;
using Should;

namespace Rockstars.DataAccess.Tests.Services.Import
{
    [TestClass]
    public class ArtistAlreadyExistsRuleTests
    {
        private readonly IRepository<Artist> _artistRepository;

        private readonly ArtistAlreadyExistRule _Rule;

        public ArtistAlreadyExistsRuleTests()
        {
            _artistRepository = A.Fake<IRepository<Artist>>();
            _Rule = new ArtistAlreadyExistRule(_artistRepository);
        }

        [TestMethod]
        public void ArtistAlreadyExits()
        {
            // assign
            var artistName = "Rolling Stones";
            var artist = new Artist {Id = 1234, Name = artistName};

            A.CallTo(() => _artistRepository.Search(A<Func<Artist, bool>>._))
                .Returns(new[] {new Artist {Name = artistName}});

            // act
            var validationResult = this._Rule.IsValid(artist);

            // assert
            validationResult.Status.ShouldEqual(ValidationStatus.Failed);
        }

        [TestMethod]
        public void ArtistDoesNotExist()
        {
            // assign
            var artistName = "Rolling Stones";
            var artist = new Artist {Id = 1234, Name = artistName};

            var noResults = Enumerable.Empty<Artist>();
            A.CallTo(() => _artistRepository.Search(A<Func<Artist, bool>>._))
                .Returns(noResults);

            // act
            var validationResult = this._Rule.IsValid(artist);

            // assert
            validationResult.Status.ShouldEqual(ValidationStatus.Succeeded);
        }
    }
}
