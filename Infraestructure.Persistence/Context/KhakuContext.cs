using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Common;
using Core.Domain.Entities.GeneralMovie;
using Core.Domain.Entities.Relations;
using Core.Domain.Entities.UserThings;
using Core.Domain.Entities.WebScraping;

namespace Infraestructure.Persistence.Context
{
    public class KhakuContext : DbContext
    {
        public KhakuContext(DbContextOptions<KhakuContext> options) : base(options) { }

        public DbSet<Genre> Genre { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genre_Movie> Genre_Movie { get; set; }
        public DbSet<Movie_MovieWeb> Movie_MovieWeb { get; set; }
        public DbSet<MovieList_Movie> MovieList_Movie { get; set; }
        public DbSet<MovieList> MovieList { get; set; }
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
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        //entry.Entity.CreatedBy = "System";
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

            modelBuilder.Entity<Genre_Movie>().ToTable("Genre_Movie");
            modelBuilder.Entity<Movie_MovieWeb>().ToTable("Movie_MovieWeb");
            modelBuilder.Entity<MovieList_Movie>().ToTable("MovieList_Movie");
            
            modelBuilder.Entity<MovieList>().ToTable("MovieList");
            modelBuilder.Entity<Recents>().ToTable("Recents");

            modelBuilder.Entity<MovieWeb>().ToTable("MovieWeb");
            modelBuilder.Entity<ScrapPage>().ToTable("ScrapPage");
            #endregion

            #region PK's
            modelBuilder.Entity<Genre>().HasKey(x => x.ID);
            modelBuilder.Entity<Movie>().HasKey(x => x.ID);

            modelBuilder.Entity<Genre_Movie>().HasKey(x => x.ID);
            modelBuilder.Entity<Movie_MovieWeb>().HasKey(x => x.ID);
            modelBuilder.Entity<MovieList_Movie>().HasKey(x => x.ID);


            modelBuilder.Entity<MovieList>().HasKey(x => x.ID);
            modelBuilder.Entity<Recents>().HasKey(x => x.ID);

            modelBuilder.Entity<MovieWeb>().HasKey(x => x.ID);
            modelBuilder.Entity<ScrapPage>().HasKey(x => x.ID);
            #endregion

            //modelBuilder.Entity<CuevanaMovies>().Property(c => c.Title).IsRequired();

            #region Relations  
            modelBuilder.Entity<Genre>().HasMany<Genre_Movie>(x => x.Genre_Movie).WithOne(x => x.Genre).HasForeignKey(x => x.GenreID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movie>().HasMany<Genre_Movie>(x => x.Genre_Movie).WithOne(x => x.Movie).HasForeignKey(x => x.MovieID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movie>().HasMany<Recents>(x => x.Recents).WithOne(x => x.Movie).HasForeignKey(x => x.MovieID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movie>().HasMany<MovieList_Movie>(x => x.MovieList_Movie).WithOne(x => x.Movie).HasForeignKey(x => x.MovieID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movie>().HasMany<Movie_MovieWeb>(x => x.Movie_MovieWeb).WithOne(x => x.Movie).HasForeignKey(x => x.MovieID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ScrapPage>().HasMany<MovieWeb>(x => x.MovieWeb).WithOne(x => x.ScrapPage).HasForeignKey(x => x.ScrapPageID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovieWeb>().HasMany<Movie_MovieWeb>(x => x.Movie_MovieWeb).WithOne(x => x.MovieWeb).HasForeignKey(x => x.MovieWebID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovieList>().HasMany<MovieList_Movie>(x => x.MovieList_Movie).WithOne(x => x.MovieList).HasForeignKey(x => x.MovieListID)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

        }
    }
}
