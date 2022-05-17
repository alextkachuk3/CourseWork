using Microsoft.EntityFrameworkCore;

namespace BookingWebService.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<User>? Users { get; set; }

        public virtual DbSet<Hotel>? Hotels { get; set; }

        public virtual DbSet<HotelNumber>? HotelNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey("UserId");
                entity.Property(e => e.Login).HasMaxLength(60).IsUnicode(false);
                entity.Property(e => e.PasswordSalt).IsUnicode(false);
                entity.Property(e => e.PasswordHash).IsUnicode(false);
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("Hotels");
                entity.HasKey("HotelId");
                entity.Property(e => e.Address).HasMaxLength(60).IsUnicode(false);
                entity.Property(e => e.Name).HasMaxLength(60).IsUnicode(false);
            });

            modelBuilder.Entity<HotelNumber>(entity =>
            {
                entity.ToTable("HotelNumbers");
                entity.HasKey("HotelNumberId");
                entity.Property(e => e.Name).HasMaxLength(60).IsUnicode(false);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
