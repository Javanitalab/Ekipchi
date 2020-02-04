using System;
using System.Linq;
using Hastnama.Ekipchi.Common.Util;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Hastnama.Ekipchi.Api.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<EkipchiDbContext>();
            context.Database.EnsureCreated();

            if (!context.Roles.Any())
            {
                context.Roles.Add(new Role { Name = "Admin" });
                context.Roles.Add(new Role { Name = "User" });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new User { Email = "nimanosrati93@gmail.com", Family = "nosrati", Name = "nima", IsEmailConfirmed = true, ConfirmCode = "1234", Username = "nosratiz", Password = StringUtil.HashPass("nima1234!"), CreateDateTime = DateTime.Now, ExpiredVerificationCode = DateTime.Now.AddDays(2), Mobile = "09107602786" });
                context.Users.Add(new User { Email = "alireza.ra1996@gmail.com", Family = "nobahari", Name = "alireza", IsEmailConfirmed = true, ConfirmCode = "1234", Username = "naser", Password = StringUtil.HashPass("1234qwer"), CreateDateTime = DateTime.Now, ExpiredVerificationCode = DateTime.Now.AddDays(2) });
                context.SaveChanges();
            }


        }
    }
}