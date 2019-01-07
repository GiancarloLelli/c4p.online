using cfp.online.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace cfp.online.Persistance
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<ProposalModel> Poposals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProposalModel>()
                        .HasKey(p => p.Id);

            modelBuilder.Entity<ProposalModel>()
                        .Property(p => p.Website)
                        .HasConversion(u => u.ToString(), u => new Uri(u));

            modelBuilder.Entity<ProposalModel>()
                        .Property(p => p.CreatedOn)
                        .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}
