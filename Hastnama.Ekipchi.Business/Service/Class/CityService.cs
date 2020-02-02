using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.City;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Hastnama.GuitarIranShop.DataAccess.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class CityService : Repository<EkipchiDbContext, City>, ICityService
    {
        private readonly IMapper _mapper;

        public CityService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<CityDto>>> List(PagingOptions pagingOptions,
            FilterCityQueryDto filterQueryDto)
        {
            var cities = await WhereAsyncAsNoTracking(c =>
                    (string.IsNullOrEmpty(filterQueryDto.Name) ||
                     c.Name.ToLower().Contains(filterQueryDto.Name.ToLower())
                     && (string.IsNullOrEmpty(filterQueryDto.CountyName) ||
                         c.County.Name.ToLower().Contains(filterQueryDto.CountyName.ToLower()))), pagingOptions,
                c => c.County, c => c.Regions);

            return Result<PagedList<CityDto>>.SuccessFull(cities.MapTo<CityDto>(_mapper));
        }

        public async Task<Result> Update(UpdateCityDto updateCityDto)
        {
            var duplicateCity = await FirstOrDefaultAsyncAsNoTracking(c => c.Name == updateCityDto.Name);
            if (duplicateCity != null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateCityName}));

            var city = await FirstOrDefaultAsync(c => c.Id == updateCityDto.Id);
            _mapper.Map(updateCityDto, city);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<CityDto>> Create(CreateCityDto createCityDto)
        {
            var duplicateCity = await FirstOrDefaultAsyncAsNoTracking(c => c.Name == createCityDto.Name);
            if (duplicateCity != null)
                return Result<CityDto>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateCityName}));

            var city = _mapper.Map(createCityDto, new City());
            await AddAsync(city);
            await Context.SaveChangesAsync();

            return Result<CityDto>.SuccessFull(_mapper.Map<CityDto>(city));
        }

        public async Task<Result<CityDto>> Get(int id)
        {
            var city = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id, c => c.County);
            if (city == null)
                return Result<CityDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.CityNotFound}));

            return Result<CityDto>.SuccessFull(_mapper.Map<CityDto>(city));
        }
    }
}