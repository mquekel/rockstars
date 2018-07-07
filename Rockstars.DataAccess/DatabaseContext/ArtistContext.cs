using Microsoft.EntityFrameworkCore;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.DatabaseContext
{
    public class ArtistContext : DbContext
    {
        public ArtistContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }
    }
}
