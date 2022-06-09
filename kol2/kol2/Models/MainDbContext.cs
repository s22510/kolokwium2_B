using kol2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zad8.Models
{
    public class MainDbContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Musician_Track> Musician_Tracks { get; set; }
        public DbSet<MusicLabel> MusicLabels { get; set; }
        public DbSet<Track> Tracks { get; set; }

        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        protected MainDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Album>(p =>
            {
                p.HasKey(e => e.IdAlbum);
                p.Property(e => e.AlbumName).IsRequired().HasMaxLength(30);
                p.Property(e => e.PublishDate).IsRequired();
                p.Property(e => e.IdMusicLabel).IsRequired();

                p.HasOne(e => e.MusicLabel).WithMany(p => p.Albums).HasForeignKey(e => e.IdMusicLabel);

                p.HasData(
                    new Album { IdAlbum = 1, AlbumName = "Jan", PublishDate = DateTime.Parse("2000-01-01"), IdMusicLabel = 1 },
                    new Album { IdAlbum = 2, AlbumName = "Jan", PublishDate = DateTime.Parse("2000-06-06"), IdMusicLabel = 2 }
                );
            });
            modelBuilder.Entity<Musician>(p =>
            {
                p.HasKey(e => e.IdMusician);
                p.Property(e => e.FirstName).IsRequired().HasMaxLength(30);
                p.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                p.Property(e => e.Nickname).HasMaxLength(20);

                p.HasData(
                    new Musician { IdMusician = 1, FirstName = "Jan", LastName = "1", Nickname = "nick1" },
                    new Musician { IdMusician = 2, FirstName = "Jan", LastName = "2", Nickname = "nick2" }
                );
            });
            modelBuilder.Entity<Musician_Track>(p =>
            {
                p.HasKey(e => e.idMusician);
                p.HasKey(e => e.idTrack);

                p.HasOne(e => e.Musician).WithMany(p => p.Musician_Tracks).HasForeignKey(e => e.idMusician);
                p.HasOne(e => e.Track).WithMany(p => p.Musician_Tracks).HasForeignKey(e => e.idTrack);

                p.HasData(
                    new Musician_Track { idMusician = 1, idTrack = 1},
                    new Musician_Track { idMusician = 2, idTrack = 2}
                );

            });
            modelBuilder.Entity<MusicLabel>(p =>
            {
                p.HasKey(e => e.IdMusicLabel);
                p.Property(e => e.Name).IsRequired().HasMaxLength(50);

                p.HasData(
                    new MusicLabel { IdMusicLabel = 1, Name = "nazwa1" },
                    new MusicLabel { IdMusicLabel = 2, Name = "nazwa2" }
                );
            });
            modelBuilder.Entity<Track>(p =>
            {
                p.HasKey(e => e.IdTrack);
                p.Property(e => e.TrackName).IsRequired().HasMaxLength(20);
                p.Property(e => e.Duration).IsRequired();
                p.Property(e => e.IdMusicAlbum);

                p.HasOne(e => e.Album).WithMany(p => p.Tracks).HasForeignKey(e => e.IdTrack);

                p.HasData(
                    new Track { IdTrack = 1, TrackName = "Jan", Duration = 5.44F, IdMusicAlbum = 1 },
                    new Track { IdTrack = 2, TrackName = "Jan", Duration = 2.88F, IdMusicAlbum = 2 }
                );
            });
        }
    }
}