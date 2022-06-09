using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2.Services
{
    public interface IDbService
    {
        Task<IEnumerable<object>> GetMusician(int idMusician);
        Task SaveChanges();
        void DeleteMusician(int idMusician);
        Task<bool> checkAlbumArtist(int idMusician);
        void DeleteMusicianTracks(int idMusician);
    }
}
