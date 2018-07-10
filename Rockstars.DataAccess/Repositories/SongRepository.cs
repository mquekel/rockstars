using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Rockstars.DataAccess.DatabaseContext;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.Repositories
{
    public class SongRepository : IRepository<Song>
    {
        private const int CacheExpiryTime = 1;

        private readonly RockstarsDb _rockstarsDb;

        private readonly IMemoryCache _cachingService;

        public SongRepository(RockstarsDb rockstarsDb, IMemoryCache cachingService)
        {
            _cachingService = cachingService;
            _rockstarsDb = rockstarsDb;
        }

        public void Create(Song entity)
        {
            this._rockstarsDb.Songs.Add(entity);
            this._rockstarsDb.SaveChanges();
        }

        private bool SongAlreadyExists(Song entity)
        {
            return this.Search(song => string.Equals(song.Name, entity.Name, StringComparison.CurrentCultureIgnoreCase)).Any();
        }

        public void Create(IEnumerable<Song> entities)
        {
            foreach (var entity in entities)
            {
                this._rockstarsDb.Songs.Add(entity);
            }

            this._rockstarsDb.SaveChanges();
        }

        public Song Get(int id)
        {
            var item = this._rockstarsDb.Songs.Find(id);
            return item;
        }

        public IEnumerable<Song> GetAll()
        {
            var allSongs = this._cachingService.GetOrCreate("all-songs", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(CacheExpiryTime);
                return this._rockstarsDb.Songs.ToList();
            });
            return allSongs;
        }

        public void Update(Song song)
        {
            var item = this._rockstarsDb.Songs.Find(song.Id);
            item.Name = song.Name;
            this._rockstarsDb.Songs.Update(item);
            this._rockstarsDb.SaveChanges();
        }

        public IEnumerable<Song> Search(Func<Song, bool> query)
        {
            var entities = this.GetAll().Where(query);
            return entities;
        }

        public void Delete(int id)
        {
            var song = this.Get(id);
            if (song == null)
            {
                return;
            }

            this._rockstarsDb.Songs.Attach(song);
            this._rockstarsDb.Songs.Remove(song);
            this._rockstarsDb.SaveChanges();
        }
    }
}
