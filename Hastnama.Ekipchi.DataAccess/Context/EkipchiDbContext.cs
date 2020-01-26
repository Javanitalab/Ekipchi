using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.GuitarIranShop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.DataAccess.Context
{
    public class EkipchiDbContext : DbContext
    {
        public EkipchiDbContext()
        {

        }

        public EkipchiDbContext(DbContextOptions<EkipchiDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserToken> UserTokens { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<County> Counties { get; set; }

        public virtual DbSet<Province> Provinces { get; set; }

        public virtual DbSet<Region> Regions { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Host> Hosts { get; set; }

        public virtual DbSet<HostGallery> HostGalleries { get; set; }

        public virtual DbSet<UserMessage> UserMessages { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<Blog> Blogs { get; set; }

        public virtual DbSet<BlogCategory> BlogCategories { get; set; }

        public virtual DbSet<Faq> Faqs { get; set; }

        public virtual DbSet<EventSchedule> EventSchedules { get; set; }

        public virtual DbSet<UserInEvent> UserInEvents { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<EventGallery> EventGalleries { get; set; }

        public virtual DbSet<UserStatistic> UserStatistics { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<UserInGroup> UserInGroups { get; set; }

        public virtual DbSet<HostCategory> HostCategories { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<HostAvailableDate> HostAvailableDates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMessage>()
                .HasOne(u => u.ReceiverUser).WithMany(u => u.ReceiverMessages).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}