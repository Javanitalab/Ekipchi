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


    }
}