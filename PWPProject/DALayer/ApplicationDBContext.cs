using CommonLibrary.BusinessEntities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Common.BusinessEntities;
using BLLayer;

namespace DALayer
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Video> Video { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public DbSet<BookMark> BookMark { get; set; }
        public DbSet<ApplicationStats> Stats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Vote>()
                .HasOne(b => b.Video)
                .WithMany(v => v.Vote)
                .HasForeignKey(b => b.Id)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<BookMark>()
                .HasOne(b => b.Video)
                .WithMany(v => v.BookMark)
                .HasForeignKey(b => b.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UploadedVideos)
                .WithOne(v => v.User)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
