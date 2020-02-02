using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.DataAccess.Context;

namespace Hastnama.Ekipchi.Business.Service.Class
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly EkipchiDbContext _context;
        private readonly IMapper _mapper;
        private bool _disposed;

        #region private

        private IUserService _userService;
        private ICityService _cityService;
        private ICountyService _countyService;
        private IProvinceService _provinceService;
        private IRegionService _regionService;
        private IBlogService _blogService;
        private IBlogCategoryService _blogCategoryService;
        private ICategoryService _categoryService;
        private ICommentService _commentService;
        private IFaqService _faqServicee;
        private ICouponService _couponService;
        private IEventService _eventService;

        #endregion

        public UnitOfWork(EkipchiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Services

        public IUserService UserService => _userService = _userService ?? new UserService(_context,_mapper);
        public ICityService CityService => _cityService = _cityService ?? new CityService(_context,_mapper);
        public ICountyService CountyService => _countyService = _countyService ?? new CountyService(_context,_mapper);
        public IProvinceService ProvinceService => _provinceService = _provinceService ?? new ProvinceService(_context,_mapper);
        public IRegionService RegionService => _regionService = _regionService ?? new RegionService(_context,_mapper);
        public IBlogService BlogService => _blogService = _blogService ?? new BlogService(_context,_mapper);
        public IBlogCategoryService BlogCategoryService => _blogCategoryService = _blogCategoryService ?? new BlogCategoryService(_context,_mapper);
        public ICategoryService CategoryService => _categoryService = _categoryService ?? new CategoryService(_context,_mapper);
        public ICommentService CommentService => _commentService = _commentService ?? new CommentService(_context,_mapper);
        public IFaqService FaqService => _faqServicee = _faqServicee ?? new FaqService(_context,_mapper);
        public ICouponService CouponService => _couponService = _couponService ?? new CouponService(_context,_mapper);
        
        public IEventService EventService => _eventService = _eventService ?? new EventService(_context, _mapper);

        #endregion Services

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}