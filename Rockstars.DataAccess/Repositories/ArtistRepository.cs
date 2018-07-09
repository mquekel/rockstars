using System;
using System.Collections.Generic;
using System.Linq;
using Rockstars.DataAccess.DatabaseContext;
using Rockstars.DataAccess.Exceptions;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.Repositories
{
    public class ArtistRepository : IRepository<Artist>
    {
        private readonly RockstarsDb _rockstarsDb;

        public ArtistRepository(RockstarsDb rockstarsDb)
        {
            _rockstarsDb = rockstarsDb;
        }

        public void Create(Artist entity)
        {
            if (ArtistAlreadyExists(entity))
            {
                throw new EntityAlreadyExistsException("The artist is already present.");
            }

            this._rockstarsDb.Artists.Add(entity);
            this._rockstarsDb.SaveChanges();
        }

        private bool ArtistAlreadyExists(Artist entity)
        {
            return this.Search(artist => string.Equals(artist.Name, entity.Name, StringComparison.CurrentCultureIgnoreCase)).Any();
        }

        public void Create(IEnumerable<Artist> entities)
        {
            foreach (var entity in entities)
            {
                if (this.ArtistAlreadyExists(entity))
                {
                    throw new EntityAlreadyExistsException($"The band with name {entity.Name} already exists.");
                }
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
            return this._rockstarsDb.Artists.ToList();
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
            var entities = this._rockstarsDb.Artists.Where(query);
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
