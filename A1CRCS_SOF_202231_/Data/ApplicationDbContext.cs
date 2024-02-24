using A1CRCS_SOF_202231_.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace A1CRCS_SOF_202231_.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<SiteUser> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Item>()
                .Property(i => i.ReservedBy)
                .IsRequired(false);

            builder.Entity<Item>()
                .Property(i => i.PictureContentType)
                .IsRequired(false);

            builder.Entity<Item>()
                .HasOne(i => i.Owner)
                .WithMany()
                .HasForeignKey(i => i.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(c => c.Item)
                .WithMany()
                .HasForeignKey(c => c.ItemId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Comment>()
                .HasOne(c => c.Owner)
                .WithMany()
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Item>()
                .HasMany(i => i.Comments)
                .WithOne(c => c.Item)
                .OnDelete(DeleteBehavior.Cascade);

            this.SeedUsers(builder);
            this.SeedItems(builder);
            base.OnModelCreating(builder);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            PasswordHasher<SiteUser> ph = new PasswordHasher<SiteUser>();
            byte[] data = new byte[0];
            SiteUser seedUser = new SiteUser
            {
                Id = "bd6275f9-4aea-47c4-8b3f-3181a5c211da",
                Email = "seeduser@seeduser.com",
                NormalizedEmail = "SEEDUSER@SEEDUSER.COM",
                UserName = "seeduser@seeduser.com",
                NormalizedUserName = "SEEDUSER@SEEDUSER.COM",
                FirstName = "SeedFirstName",
                LastName = "SeedLastName",
                PictureContentType = "image/jpeg",
                PictureData = data
            };
            seedUser.PasswordHash = ph.HashPassword(seedUser, "abc123");
            builder.Entity<SiteUser>().HasData(seedUser);
        }
        private void SeedItems(ModelBuilder builder)
        {
            Item item1 = new Item
            {
                Uid = "63c9f2b3-ec42-4520-a3dc-5e2bdff2050f",
                Title = "Jeans",
                Description = "Perfectly new, not used, size: M",
                Price = 5000,
                Reserved = false,
                Date = DateTime.Now,
                OwnerId = "bd6275f9-4aea-47c4-8b3f-3181a5c211da",
                Category = ItemCategory.clothes,
                ReservedBy = "",
                SequenceNum = 50
            };
            
            builder.Entity<Item>().HasData(item1);
        }
    }
}