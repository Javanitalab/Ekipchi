using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.Data.City;
using Hastnama.Ekipchi.Data.City;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Hastnama.GuitarIranShop.DataAccess.Helper;

namespace Hastnama.Ekipchi.Business.Service.Interface
{
    public interface ICityService : IRepository<City>
    {
        Task<Result<PagedList<CityDto>>> List(PagingOptions pagingOptions, FilterCityQueryDto filterQueryDto);
        Task<Result> Update(UpdateCityDto updateCityDto);
        Task<Result<CityDto>> Create(CreateCityDto dto);
        Task<Result<CityDto>> Get(int id);
    }
}