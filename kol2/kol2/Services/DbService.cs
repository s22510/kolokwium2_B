using kol2.Models;
using kol2.Models.DTOS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zad8.Models;

namespace kol2.Services
{
    public class DbService : IDbService
    {
        private MainDbContext _DbContext;
        public DbService(MainDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<bool> checkAlbumArtist(int idMusician)
        {
            var tracks = await _DbContext.Musician_Tracks.Select(a => new SomeMusician_Track
            {
                Musician = new SomeMusician { IdMusician = a.Musician.IdMusician, FirstName = a.Musician.FirstName, LastName = a.Musician.LastName, Nickname = a.Musician.Nickname },
                Track = new SomeTrack { IdTrack = a.Track.IdTrack, TrackName = a.Track.TrackName, Duration = a.Track.Duration, IdMusicAlbum = a.Track.IdMusicAlbum }
            }).ToArrayAsync();
            var track = tracks.Where(a => a.Track.IdMusicAlbum != null).ToArray();
            return track.Length>0;
        }

        public void DeleteMusician(int idMusician)
        {
            var musician = new Musician
            {
                IdMusician = idMusician
            };

            var entry = _DbContext.Entry(musician);
            entry.State = EntityState.Deleted;
        }

        public async void DeleteMusicianTracks(int idMusician)
        {
            var tracks = await _DbContext.Musician_Tracks.Select(a => new SomeMusician_Track
            {
                Musician = new SomeMusician { IdMusician = a.Musician.IdMusician, FirstName = a.Musician.FirstName, LastName = a.Musician.LastName, Nickname = a.Musician.Nickname },
                Track = new SomeTrack { IdTrack = a.Track.IdTrack, TrackName = a.Track.TrackName, Duration = a.Track.Duration, IdMusicAlbum = a.Track.IdMusicAlbum }
            }).ToArrayAsync();

            foreach(var t in tracks.Select(a=>a.Track)){
                var track = new Track
                {
                    IdTrack = t.IdTrack
                };
                var entry = _DbContext.Entry(track);
                entry.State = EntityState.Deleted;
            }
        }

        public async Task<IEnumerable<object>> GetMusician(int idMusician)
        {
            var musician = await _DbContext.Musician_Tracks.Select(a=> new SomeMusician_Track
            {
                Musician = new SomeMusician {IdMusician=a.Musician.IdMusician, FirstName = a.Musician.FirstName, LastName = a.Musician.LastName, Nickname = a.Musician.Nickname},
                Track = new SomeTrack { IdTrack = a.Track.IdTrack, TrackName = a.Track.TrackName, Duration = a.Track.Duration, IdMusicAlbum = a.Track.IdMusicAlbum }
            }).ToArrayAsync();
            return musician.Where(a=>a.Musician.IdMusician==idMusician);
        }

        public async Task SaveChanges()
        {
            await _DbContext.SaveChangesAsync();
        }
    }
}
