using K_haku.Domain.Common;
using K_haku.Core.Domain.Entities.Cuevana;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K_haku.Infraestructure.Persistence.Contexts
{
    public class K_hakuContext : DbContext
    {
        public K_hakuContext(DbContextOptions<K_hakuContext> options) : base(options) { }

        public DbSet<CuevanaMovies> cuevanaMovies { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "WebScrapping"; 
                        break;
                    case EntityState.Modified:
                        entry.Entity.Created = DateTime.Now;
                        //entry.Entity.CreatedBy = "System";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CuevanaMovies>().ToTable("CuevanaMovies");


            modelBuilder.Entity<CuevanaMovies>().HasKey(cMV => cMV.ID);


            modelBuilder.Entity<CuevanaMovies>().Property(c => c.Title).IsRequired();
            modelBuilder.Entity<CuevanaMovies>().Property(c => c.Photo).IsRequired();
            modelBuilder.Entity<CuevanaMovies>().Property(c => c.Link).IsRequired();
            modelBuilder.Entity<CuevanaMovies>().Property(c => c.Age).IsRequired();
        }
    }
}
