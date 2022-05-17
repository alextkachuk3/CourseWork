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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey("Id");
                entity.Property(e => e.Login).HasMaxLength(60).IsUnicode(false);
                entity.Property(e => e.PasswordSalt).IsUnicode(false);
                entity.Property(e => e.PasswordHash).IsUnicode(false);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
