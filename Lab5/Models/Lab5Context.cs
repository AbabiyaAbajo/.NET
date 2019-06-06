using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lab5a.Models
{
    public partial class Lab5Context : DbContext
    {
        public Lab5Context()
        {
        }

        public Lab5Context(DbContextOptions<Lab5Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Photos> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=abajoserv.database.windows.net;User Id=ab;Password=Yes01yes!;Database=Lab5; Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Photos>(entity =>
            {
                entity.HasKey(e => e.PhotoId)
                    .HasName("PK__Photos__21B7B5E22C8DEC1C");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(2048);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(2048);
            });
        }
    }
}
