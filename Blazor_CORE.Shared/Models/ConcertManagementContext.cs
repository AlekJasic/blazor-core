using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Blazor_CORE.Shared.Models
{
    public partial class ConcertManagementContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ConcertManagementContext(IConfiguration configuration) {
            this.configuration = configuration;
        }
        public virtual DbSet<ConcertDetails> ConcertDetails { get; set; }
        public virtual DbSet<ConcertMasters> ConcertMasters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connString = configuration.GetConnectionString("ConcertManagementDatabase");
                optionsBuilder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConcertDetails>(entity =>
            {
                entity.HasKey(e => e.ConcertDetailNo);

                entity.Property(e => e.ArtistName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ConcertMasters>(entity =>
            {
                entity.HasKey(e => e.ConcertNo);

                entity.Property(e => e.ConcertDate)
                    .HasColumnName("ConcertDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.HallId)
                    .IsRequired()
                    .HasColumnName("HallID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TicketServiceName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
