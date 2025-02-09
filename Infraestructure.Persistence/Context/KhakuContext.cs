using Core.Domain.Common;
using Core.Domain.Entities.Movie;
using Core.Domain.Entities.Relations;
using Core.Domain.Entities.UserThings;
using Core.Domain.Entities.WebScraping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Context
{
    public class KhakuContext : DbContext
    {
        public KhakuContext(DbContextOptions<KhakuContext> options) : base(options) { }

        public DbSet<Genre> Genre { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<GenreMovie> Genre_Movie { get; set; }
        public DbSet<MovieMovieWeb> Movie_MovieWeb { get; set; }
        public DbSet<MovieListMovie> MovieList_Movie { get; set; }
        public DbSet<ShareList> ShareList { get; set; }
        public DbSet<Recents> Recents { get; set; }
        public DbSet<MovieWeb> MovieWeb { get; set; }
        public DbSet<ScrapPage> ScrapPage { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "System";
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedby = "System";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region DBNames
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Movie>().ToTable("Movie");

            modelBuilder.Entity<GenreMovie>().ToTable("Genre_Movie");
            modelBuilder.Entity<MovieMovieWeb>().ToTable("Movie_MovieWeb");
            modelBuilder.Entity<MovieListMovie>().ToTable("MovieList_Movie");

            modelBuilder.Entity<ShareList>().ToTable("ShareList");
            modelBuilder.Entity<Recents>().ToTable("Recents");

            modelBuilder.Entity<MovieWeb>().ToTable("MovieWeb");
            modelBuilder.Entity<ScrapPage>().ToTable("ScrapPage");
            #endregion

            #region PK's
            modelBuilder.Entity<Genre>().HasKey(x => x.ID);
            modelBuilder.Entity<Movie>().HasKey(x => x.ID);

            modelBuilder.Entity<GenreMovie>().HasKey(x => x.ID);
            modelBuilder.Entity<MovieMovieWeb>().HasKey(x => x.ID);
            modelBuilder.Entity<MovieListMovie>().HasKey(x => x.ID);


            modelBuilder.Entity<ShareList>().HasKey(x => x.ID);
            modelBuilder.Entity<Recents>().HasKey(x => x.ID);

            modelBuilder.Entity<MovieWeb>().HasKey(x => x.ID);
            modelBuilder.Entity<ScrapPage>().HasKey(x => x.ID);
            #endregion

            #region Relations  
            modelBuilder.Entity<Genre>().HasMany(x => x.GenreMovie).WithOne(x => x.Genre).HasForeignKey(x => x.GenreID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movie>().HasMany(x => x.GenreMovie).WithOne(x => x.Movie).HasForeignKey(x => x.MovieID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movie>().HasMany(x => x.Recents).WithOne(x => x.Movie).HasForeignKey(x => x.MovieID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movie>().HasMany(x => x.MovieMovieWeb).WithOne(x => x.Movie).HasForeignKey(x => x.MovieID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movie>().HasMany(x => x.MovieMovieWeb).WithOne(x => x.Movie).HasForeignKey(x => x.MovieID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ScrapPage>().HasMany(x => x.MovieWeb).WithOne(x => x.ScrapPage).HasForeignKey(x => x.ScrapPageID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovieWeb>().HasMany(x => x.Movie_MovieWeb).WithOne(x => x.MovieWeb).HasForeignKey(x => x.MovieWebID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShareList>().HasMany(x => x.MovieListMovie).WithOne(x => x.ShareList).HasForeignKey(x => x.ShareListID)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
