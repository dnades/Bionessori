using Bionessori.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Core.Data {
    public class ApplicationDbContext : DbContext {
        public DbSet<Request> Requests { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<MultepleContextTable>()
            .HasKey(t => new { t.RequestId });

            modelBuilder.Entity<MultepleContextTable>()
                .HasOne(sc => sc.Request)
                .WithMany(s => s.MultepleContextTables);            
        }
    }
}
