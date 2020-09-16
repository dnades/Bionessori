using Bionessori.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Core.Data {
    public class ApplicationDbContext : DbContext {
        public DbSet<Request> Requests { get; set; }    // Таблица заявок.

        public DbSet<Werehouse> Werehouses { get; set; }    // Таблица складов.

        public DbSet<Provider> Providers { get; set; }  // Таблица поставщиков.

        public DbSet<CommerceOffer> CommerceOffers { get; set; }    // Таблица предложений поставщикам.

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<MultepleContextTable>()
            .HasKey(t => new { t.RequestId });

            modelBuilder.Entity<MultepleContextTable>()
                .HasOne(sc => sc.Request)
                .WithMany(s => s.MultepleContextTables);

            modelBuilder.Entity<MultepleContextTable>()
                .HasOne(sc => sc.Werehouse)
                .WithMany(s => s.MultepleContextTables);

            modelBuilder.Entity<MultepleContextTable>()
                .HasOne(sc => sc.Provider)
                .WithMany(s => s.MultepleContextTables);

            modelBuilder.Entity<MultepleContextTable>()
                .HasOne(sc => sc.CommerceOffer)
                .WithMany(s => s.MultepleContextTables);
        }
    }
}
