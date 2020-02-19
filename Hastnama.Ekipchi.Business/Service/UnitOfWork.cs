using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Class;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.DataAccess.Context;

namespace Hastnama.Ekipchi.Business.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        #region private

        private readonly EkipchiDbContext _context;
        private readonly IMapper _mapper;
        private bool _disposed;
        private IUserService _userService;
        private IUserTokenService _userTokenService;
        private IUserFilesService _filesService;
        private ICityService _cityService;
        private ICountyService _countyService;
        private IProvinceService _provinceService;
        private IFinancialTransactionService _financialTransactionService;
        private IRegionService _regionService;
        private IBlogService _blogService;
        private IBlogCategoryService _blogCategoryService;
        private ICategoryService _categoryService;
        private ICommentService _commentService;
        private IFaqService _faqServicee;
        private ICouponService _couponService;
        private IEventService _eventService;
        private IGroupService _groupService;
        private IUserMessageService _userMessageService;
        private IMessageService _messageService;
        private IRolePermissionService _rolePermissionService;
        private IRoleService _roleService;
        private IPermissionService _permissionService;
        private IUserInRoleService _userInRoleService;
        private IHostService _hostService;

        #endregion

        public UnitOfWork(EkipchiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Services

        public IFinancialTransactionService FinancialTransactionService => _financialTransactionService ??= new FinancialTransactionService(_context, _mapper);
        public IUserService UserService => _userService ??= new UserService(_context, _mapper);

        public IUserTokenService UserTokenService =>
            _userTokenService ??= new UserTokenService(_context);

        public IHostService HostService => _hostService ??= new HostService(_context, _mapper);
        public ICityService CityService => _cityService ??= new CityService(_context, _mapper);
        public ICountyService CountyService => _countyService ??= new CountyService(_context, _mapper);
        public IProvinceService ProvinceService => _provinceService ??= new ProvinceService(_context, _mapper);
        public IRegionService RegionService => _regionService ??= new RegionService(_context, _mapper);
        public IBlogService BlogService => _blogService ??= new BlogService(_context, _mapper);

        public IBlogCategoryService BlogCategoryService =>
            _blogCategoryService ??= new BlogCategoryService(_context, _mapper);

        public ICategoryService CategoryService => _categoryService ??= new CategoryService(_context, _mapper);
        public ICommentService CommentService => _commentService ??= new CommentService(_context, _mapper);
        public IFaqService FaqService => _faqServicee ??= new FaqService(_context, _mapper);
        public ICouponService CouponService => _couponService ??= new CouponService(_context, _mapper);
        public IEventService EventService => _eventService ??= new EventService(_context, _mapper);
        public IGroupService GroupService => _groupService ??= new GroupService(_context, _mapper);
        public IUserMessageService UserMessageService => _userMessageService ??= new UserMessageService(_context);
        public IMessageService MessageService => _messageService ??= new MessageService(_context);

        public IRolePermissionService RolePermissionService =>
            _rolePermissionService ??= new RolePermissionService(_context);

        public IUserInRoleService UserInRoleService => _userInRoleService ??= new UserInRoleService(_context);
        public IPermissionService PermissionService => _permissionService ??= new PermissionService(_context);
        public IRoleService RoleService => _roleService ??= new RoleService(_context);

        public IUserFilesService FilesService => _filesService = _filesService ?? new UserFilesService(_context);

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