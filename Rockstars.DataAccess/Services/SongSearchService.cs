using System.Collections.Generic;
using Rockstars.DataAccess.Repositories;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.Services
{
    public class SongSearchService : ISongSearchService
    {
        private readonly IRepository<Song> _songRepository;

        public SongSearchService(IRepository<Song> songRepository)
        {
            _songRepository = songRepository;
        }

        public IEnumerable<Song> SearchByGenre(string genre)
        {
            var songs = this._songRepository.Search(q => q.Genre.ToLower().Contains(genre.ToLower()));
            return songs;
        }

        public IEnumerable<Song> SearchByArtist(string artist)
        {
            //@todo: should probably add some caching here for performance.
            var songs = this._songRepository.Search(q => q.Artist.ToLower().Contains(artist.ToLower()));
            return songs;
        }
    }
}
