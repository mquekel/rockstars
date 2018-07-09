using System;
using System.Collections.Generic;
using System.Linq;
using Rockstars.DataAccess.DatabaseContext;
using Rockstars.DataAccess.Exceptions;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.Repositories
{
    public class SongRepository : IRepository<Song>
    {
        private readonly RockstarsDb _rockstarsDb;

        public SongRepository(RockstarsDb rockstarsDb)
        {
            _rockstarsDb = rockstarsDb;
        }

        public void Create(Song entity)
        {
            if (SongAlreadyExists(entity))
            {
                throw new EntityAlreadyExistsException("The Song is already present.");
            }

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
                if (this.SongAlreadyExists(entity))
                {
                    throw new EntityAlreadyExistsException($"The song with name {entity.Name} already exists.");
                }
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
            return this._rockstarsDb.Songs.ToList();
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
            var entities = this._rockstarsDb.Songs.Where(query);
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
