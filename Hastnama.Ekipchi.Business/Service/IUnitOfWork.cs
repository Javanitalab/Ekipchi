using System;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Business.Service.Interface;

namespace Hastnama.Ekipchi.Business.Service
{
    public interface IUnitOfWork : IDisposable
    {
        #region Services

        IUserService UserService { get; }
        ICityService CityService { get; }
        ICountyService CountyService { get; }
        IProvinceService ProvinceService { get; }
        IRegionService RegionService { get; }
        IBlogService BlogService { get; }
        IBlogCategoryService BlogCategoryService { get; }
        ICategoryService CategoryService { get; }
        ICommentService CommentService { get; }
        IFaqService FaqService { get; }
        ICouponService CouponService { get; }
        IEventService EventService { get; }
        IGroupService GroupService { get; }
        IEventGalleryService EventGalleryService { get; }
        IEventScheduleService EventScheduleService { get; }
        IUserMessageService UserMessageService { get; }
        IMessageService MessageService { get; }

        IRolePermissionService RolePermissionService { get; }

        IUserInRoleService UserInRoleService { get; }

        IPermissionService PermissionService { get; }

        IRoleService RoleService { get; }

        #endregion Services

        Task SaveChangesAsync();
    }
}