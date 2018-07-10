using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Rockstars.DataAccess.DatabaseContext;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.Repositories
{
    public class ArtistRepository : IRepository<Artist>
    {
        private readonly RockstarsDb _rockstarsDb;

        private readonly IMemoryCache _cachingService;

        public double CacheExpiryTime = 3;

        public ArtistRepository(RockstarsDb rockstarsDb, IMemoryCache cachingService)
        {
            _cachingService = cachingService;
            _rockstarsDb = rockstarsDb;
        }

        public void Create(Artist entity)
        {
            this._rockstarsDb.Artists.Add(entity);
            this._rockstarsDb.SaveChanges();
        }


        public void Create(IEnumerable<Artist> entities)
        {
            foreach (var entity in entities)
            {
                this._rockstarsDb.Artists.Add(entity);
            }

            this._rockstarsDb.SaveChanges();
        }

        public Artist Get(int id)
        {
            var item = this._rockstarsDb.Artists.Find(id);
            return item;
        }

        public IEnumerable<Artist> GetAll()
        {
            var allArtists = this._cachingService.GetOrCreate("all-artists", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(CacheExpiryTime);
                return this._rockstarsDb.Artists.ToList();
            });
            return allArtists;
        }

        public void Update(Artist artist)
        {
            var item = this._rockstarsDb.Artists.Find(artist.Id);
            item.Name = artist.Name;
            this._rockstarsDb.Artists.Update(item);
            this._rockstarsDb.SaveChanges();
        }

        public IEnumerable<Artist> Search(Func<Artist, bool> query)
        {
            var entities = this.GetAll().Where(query);
            return entities;
        }

        public void Delete(int id)
        {
            var artist = this.Get(id);
            if (artist == null)
            {
                return;
            }

            this._rockstarsDb.Artists.Attach(artist);
            this._rockstarsDb.Artists.Remove(artist);
            this._rockstarsDb.SaveChanges();
        }
    }
}
