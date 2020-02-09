using System;
using System.Linq;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Common.Util;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable StringLiteralTypo

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
                context.Roles.Add(new Role {Name = "Admin"});
                context.Roles.Add(new Role {Name = "User"});
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Email = "nimanosrati93@gmail.com", Family = "nosrati", Name = "nima", IsEmailConfirmed = true,
                    ConfirmCode = "1234", Username = "nosratiz", Password = StringUtil.HashPass("nima1234!"),
                    CreateDateTime = DateTime.Now, ExpiredVerificationCode = DateTime.Now.AddDays(2),
                    Mobile = "09107602786", Status = UserStatus.Active
                });
                context.Users.Add(new User
                {
                    Email = "mohammad.javan.t@gmail.com", Family = "javani", Name = "mohammad", IsEmailConfirmed = true,
                    ConfirmCode = "1234", Username = "nosratiz", Password = StringUtil.HashPass("123QWE"),
                    CreateDateTime = DateTime.Now, ExpiredVerificationCode = DateTime.Now.AddDays(2),
                    Mobile = "09367572636", Status = UserStatus.Active
                });
                context.Users.Add(new User
                {
                    Email = "alireza.ra1996@gmail.com", Family = "nobahari", Name = "alireza", IsEmailConfirmed = true,
                    ConfirmCode = "1234", Username = "naser", Password = StringUtil.HashPass("1234qwer"),
                    CreateDateTime = DateTime.Now, ExpiredVerificationCode = DateTime.Now.AddDays(2),
                    Status = UserStatus.Active
                });
                context.SaveChanges();
            }

            if (!context.Permissions.Any())
            {
                context.Permissions.Add(new Permission
                    {
                        Name = "Blog",
                        Description = "دسترسی های مربوط به موجودیت : بلاگ ",
                        Children =
                        {
                            new Permission
                            {
                                Name = "Create", Description = "اضافه کردن"
                            },
                            new Permission
                            {
                                Name = "Update", Description = "تصحیح کردن",
                                RolePermissions = {new RolePermission {RoleId = 1}},
                            },
                            new Permission
                            {
                                Name = "Delete", Description = "پاک کردن",
                                RolePermissions = {new RolePermission {RoleId = 1}}
                            },
                            new Permission
                            {
                                Name = "Read", Description = "خواندن",
                                RolePermissions = {new RolePermission {RoleId = 1}}
                            }
                        }
                    }
                );
                context.Permissions.Add(new Permission
                    {
                        Name = "BlogCategory",
                        Description = "دسترسی های مربوط به موجودیت : دسته بندی بلاگ ",
                        Children =
                        {
                            new Permission
                            {
                                Name = "Create", Description = "اضافه کردن"
                            },
                            new Permission
                            {
                                Name = "Update", Description = "تصحیح کردن",
                                RolePermissions = {new RolePermission {RoleId = 1}},
                            },
                            new Permission
                            {
                                Name = "Delete", Description = "پاک کردن",
                                RolePermissions = {new RolePermission {RoleId = 1}}
                            },
                            new Permission
                            {
                                Name = "Read", Description = "خواندن",
                                RolePermissions = {new RolePermission {RoleId = 1}}
                            }
                        }
                    }
                );
                context.Permissions.Add(new Permission
                {
                    Name = "Category",
                    Description = "دسترسی های مربوط به موجودیت : گروه بندی ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "City",
                    Description = "دسترسی های مربوط به موجودیت : شهر ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "Comment",
                    Description = "دسترسی های مربوط به موجودیت : نظر ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "County",
                    Description = "دسترسی های مربوط به موجودیت : شهرستان ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "Coupan",
                    Description = "دسترسی های مربوط به موجودیت : کد تخفیف ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "Event",
                    Description = "دسترسی های مربوط به موجودیت : رویداد ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "EventGallery",
                    Description = "دسترسی های مربوط به موجودیت : گالری رویداد ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "EventSchedule",
                    Description = "دسترسی های مربوط به موجودیت : تقویم رویداد ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "Faq",
                    Description = "دسترسی های مربوط به موجودیت : پرسش و پاسخ ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "Group",
                    Description = "دسترسی های مربوط به موجودیت : گروه ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "Host",
                    Description = "دسترسی های مربوط به موجودیت : میزبان ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "HostGallery",
                    Description = "دسترسی های مربوط به موجودیت : تصاویر میزبان ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "Message",
                    Description = "دسترسی های مربوط به موجودیت : پیام ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "Permission",
                    Description = "دسترسی های مربوط به موجودیت : دسترسی ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "Province",
                    Description = "دسترسی های مربوط به موجودیت : استان ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "Region",
                    Description = "دسترسی های مربوط به موجودیت : منطقه ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "Role",
                    Description = "دسترسی های مربوط به موجودیت : نقش ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "User",
                    Description = "دسترسی های مربوط به موجودیت : کاربر ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Create", Description = "اضافه کردن"
                        },
                        new Permission
                        {
                            Name = "Update", Description = "تصحیح کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}},
                        },
                        new Permission
                        {
                            Name = "Delete", Description = "پاک کردن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "UserStatistics",
                    Description = "دسترسی های مربوط به موجودیت : آمارکاربر ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Read", Description = "خواندن",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        }
                    }
                });
                context.Permissions.Add(new Permission
                {
                    Name = "UserType",
                    Description = "دسترسی های مربوط به موجودیت : تنوع کاربر ",
                    Children =
                    {
                        new Permission
                        {
                            Name = "Admin", Description = "کاربر ناظر",
                            RolePermissions = {new RolePermission {RoleId = 1}}
                        },
                        new Permission
                        {
                            Name = "User", Description = "کاربر عادی",
                            RolePermissions = {new RolePermission {RoleId = 2}}
                        }
                    }
                });
                context.SaveChanges();
            }
        }
    }
}