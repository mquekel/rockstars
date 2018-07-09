using System.Collections.Generic;
using Rockstars.Domain.Entities;

namespace Rockstars.DataAccess.Services
{
    public interface ISongSearchService
    {
        IEnumerable<Song> SearchByGenre(string genre);

        IEnumerable<Song> SearchByArtist(string artist);
    }
}