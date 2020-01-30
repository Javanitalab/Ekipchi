using Hastnama.Ekipchi.Business.Service.Interface;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    public class CityController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


    }
}