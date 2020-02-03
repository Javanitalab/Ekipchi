using System;
using System.Threading.Tasks;

namespace Hastnama.Ekipchi.Business.Service.Interface
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
        IEventGalleryService EventGalleryService { get; }

        #endregion Services

        Task SaveChangesAsync();
    }
}