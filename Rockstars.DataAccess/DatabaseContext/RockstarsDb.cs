using Microsoft.EntityFrameworkCore;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.DatabaseContext
{
    public class RockstarsDb : DbContext
    {
        public RockstarsDb(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Song> Songs { get; set; }
    }
}
