using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Country;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class CountyService : Repository<EkipchiDbContext, County>, ICountyService
    {
        private readonly IMapper _mapper;

        public CountyService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<CountyDto>>> List(FilterCountyQueryDto filterQueryDto)
        {
            var counties = await WhereAsyncAsNoTracking(c =>
                    (string.IsNullOrEmpty(filterQueryDto.Keyword) ||
                     c.Name.ToLower().Contains(filterQueryDto.Keyword.ToLower())
                     && (string.IsNullOrEmpty(filterQueryDto.Keyword.ToLower()) ||
                         c.Province.Name.ToLower().Contains(filterQueryDto.Keyword.ToLower()))), filterQueryDto,
                c => c.Province);


            return Result<PagedList<CountyDto>>.SuccessFull(counties.MapTo<CountyDto>(_mapper));
        }

        public async Task<Result> Update(UpdateCountyDto updateCountyDto)
        {

            var county = await FirstOrDefaultAsync(c => c.Id == updateCountyDto.Id);

            if (county.ProvinceId != updateCountyDto.ProvinceId)
            {
                var province = await Context.Provinces.FirstOrDefaultAsync(u => u.Id == updateCountyDto.ProvinceId);

                if (province == null)
                    return Result.Failed(new BadRequestObjectResult(new ApiMessage
                        {Message = PersianErrorMessage.InvalidProvinceId}));
                
                county.Province = province;
            }

            _mapper.Map(updateCountyDto, county);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<CountyDto>> Create(CreateCountyDto createCountyDto)
        {

            var province = await Context.Provinces.FirstOrDefaultAsync(u => u.Id == createCountyDto.ProvinceId);

            if (province == null)
                return Result<CountyDto>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.InvalidProvinceId}));

            var county = _mapper.Map<County>(createCountyDto);
            county.Province = province;
            
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