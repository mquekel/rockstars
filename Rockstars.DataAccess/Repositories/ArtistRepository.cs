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
        private readonly ArtistContext _artistContext;

        public ArtistRepository(ArtistContext artistContext)
        {
            _artistContext = artistContext;
        }

        public void Create(Artist entity)
        {
            if (ArtistAlreadyExists(entity))
            {
                throw new EntityAlreadyExistsException("The artist is already present.");
            }

            this._artistContext.Artists.Add(entity);
            this._artistContext.SaveChanges();
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
                this._artistContext.Artists.Add(entity);
            }

            this._artistContext.SaveChanges();
        }

        public Artist Get(int id)
        {
            var item = this._artistContext.Artists.Find(id);
            return item;
        }

        public IEnumerable<Artist> GetAll()
        {
            return this._artistContext.Artists.ToList();
        }

        public void Update(Artist artist)
        {
            var item = this._artistContext.Artists.Find(artist.Id);
            item.Name = artist.Name;
            this._artistContext.Artists.Update(item);
            this._artistContext.SaveChanges();
        }

        public IEnumerable<Artist> Search(Func<Artist, bool> query)
        {
            var entities = this._artistContext.Artists.Where(query);
            return entities;
        }

        public void Delete(int id)
        {
            var artist = this.Get(id);
            if (artist == null)
            {
                return;
            }

            this._artistContext.Artists.Attach(artist);
            this._artistContext.Artists.Remove(artist);
            this._artistContext.SaveChanges();
        }
    }
}
