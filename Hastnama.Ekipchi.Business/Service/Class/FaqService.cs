using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Faq;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;

using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class FaqService : Repository<EkipchiDbContext, Faq>, IFaqService
    {
        private readonly IMapper _mapper;

        public FaqService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<FaqDto>>> List(FilterFaqQueryDto filterQueryDto)
        {
            var counties = await WhereAsyncAsNoTracking(c =>
                (string.IsNullOrEmpty(filterQueryDto.Question) ||
                 c.Question.ToLower().Contains(filterQueryDto.Question.ToLower())), filterQueryDto);


            return Result<PagedList<FaqDto>>.SuccessFull(counties.MapTo<FaqDto>(_mapper));
        }

        public async Task<Result> Update(UpdateFaqDto updateFaqDto)
        {
            var faq = await FirstOrDefaultAsync(c => c.Id == updateFaqDto.Id);
            _mapper.Map(updateFaqDto, faq);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<FaqDto>> Create(CreateFaqDto createFaqDto)
        {
            var faq = _mapper.Map(createFaqDto, new Faq());
            await AddAsync(faq);
            await Context.SaveChangesAsync();

            return Result<FaqDto>.SuccessFull(_mapper.Map<FaqDto>(faq));
        }

        public async Task<Result<FaqDto>> Get(int id)
        {
            var faq = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id);
            if (faq == null)
                return Result<FaqDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = ResponseMessage.FaqNotFound}));

            return Result<FaqDto>.SuccessFull(_mapper.Map<FaqDto>(faq));
        }
        
        public async Task<Result> Delete(int id)
        {
            var faq = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id);
            if (faq == null)
                return Result.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = ResponseMessage.FaqNotFound}));

            Delete(faq);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

    }
}