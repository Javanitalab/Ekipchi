using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Country;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class CountyService : Repository<EkipchiDbContext, County>, ICountyService
    {
        private readonly IMapper _mapper;

        public CountyService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<CountyDto>>> List(PagingOptions pagingOptions,
            FilterCountyQueryDto filterQueryDto)
        {
            var counties = await WhereAsyncAsNoTracking(c =>
                (string.IsNullOrEmpty(filterQueryDto.Name) || c.Name.ToLower().Contains(filterQueryDto.Name.ToLower())
                 && (string.IsNullOrEmpty(filterQueryDto.ProvinceName) ||
                     c.Province.Name.ToLower().Contains(filterQueryDto.ProvinceName.ToLower()))), pagingOptions,c=>c.Province);


            return Result<PagedList<CountyDto>>.SuccessFull(counties.MapTo<CountyDto>(_mapper));
        }

        public async Task<Result> Update(UpdateCountyDto updateCountyDto)
        {
            var duplicateCounty = await FirstOrDefaultAsyncAsNoTracking(c => c.Name == updateCountyDto.Name);
            if (duplicateCounty != null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateCountyName}));

            var county = await FirstOrDefaultAsync(c => c.Id == updateCountyDto.Id);
            _mapper.Map(updateCountyDto, county);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<CountyDto>> Create(CreateCountyDto createCountyDto)
        {
            var duplicateCounty = await FirstOrDefaultAsyncAsNoTracking(c => c.Name == createCountyDto.Name);
            if (duplicateCounty != null)
                return Result<CountyDto>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateCountyName}));

            var county = _mapper.Map(createCountyDto, new County());
            await AddAsync(county);
            await Context.SaveChangesAsync();

            return Result<CountyDto>.SuccessFull(_mapper.Map<CountyDto>(county));
        }

        public async Task<Result<CountyDto>> Get(int id)
        {
            var county = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id, c => c.Province);
            if (county == null)
                return Result<CountyDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.CountyNotFound}));

            return Result<CountyDto>.SuccessFull(_mapper.Map<CountyDto>(county));
        }
    }
}