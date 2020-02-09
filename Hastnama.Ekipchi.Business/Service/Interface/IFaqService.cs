using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Faq;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;


namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface IFaqService : IRepository<Faq>
    {
        Task<Result<PagedList<FaqDto>>> List( FilterFaqQueryDto filterQueryDto);
        Task<Result> Update(UpdateFaqDto updateFaqDto);
        Task<Result<FaqDto>> Create(CreateFaqDto dto);
        Task<Result<FaqDto>> Get(int id);
        Task<Result> Delete(int id);

    }
}