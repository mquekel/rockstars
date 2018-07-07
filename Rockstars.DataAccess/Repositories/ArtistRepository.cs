using System.Collections.Generic;
using System.Linq;
using Rockstars.DataAccess.DatabaseContext;
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
            this._artistContext.Artists.Add(entity);
            this._artistContext.SaveChanges();
        }

        public Artist Get(long id)
        {
            var item = this._artistContext.Artists.Find(id);
            return item;
        }

        public IEnumerable<Artist> GetAll()
        {
            return this._artistContext.Artists.ToList();
        }
    }
}
