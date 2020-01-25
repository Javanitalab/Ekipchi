using Hastnama.Ekipchi.DataAccess.Entities;
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
    }
}