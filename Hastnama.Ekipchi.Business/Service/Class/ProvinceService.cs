using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Province;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class ProvinceService : Repository<EkipchiDbContext, Province>, IProvinceService
    {
        private readonly IMapper _mapper;

        public ProvinceService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<ProvinceDto>>> List(PagingOptions pagingOptions,
            FilterProvinceQueryDto filterQueryDto)
        {
            var counties = await WhereAsyncAsNoTracking(c =>
                    (string.IsNullOrEmpty(filterQueryDto.Name) ||
                     c.Name.ToLower().Contains(filterQueryDto.Name.ToLower())),
                pagingOptions,c=>c.Counties);


            return Result<PagedList<ProvinceDto>>.SuccessFull(counties.MapTo<ProvinceDto>(_mapper));
        }

        public async Task<Result> Update(UpdateProvinceDto updateProvinceDto)
        {
            var duplicateProvince = await FirstOrDefaultAsyncAsNoTracking(c => c.Name == updateProvinceDto.Name);
            if (duplicateProvince != null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateProvinceName}));

            var province = await FirstOrDefaultAsync(c => c.Id == updateProvinceDto.Id);
            _mapper.Map(updateProvinceDto, province);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<ProvinceDto>> Create(CreateProvinceDto createProvinceDto)
        {
            var duplicateProvince = await FirstOrDefaultAsyncAsNoTracking(c => c.Name == createProvinceDto.Name);
            if (duplicateProvince != null)
                return Result<ProvinceDto>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateProvinceName}));

            var province = _mapper.Map<Province>(createProvinceDto);
            await AddAsync(province);
            await Context.SaveChangesAsync();

            return Result<ProvinceDto>.SuccessFull(_mapper.Map<ProvinceDto>(province));
        }

        public async Task<Result<ProvinceDto>> Get(int id)
        {
            var province = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id, c => c.Counties);
            if (province == null)
                return Result<ProvinceDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.ProvinceNotFound}));

            return Result<ProvinceDto>.SuccessFull(_mapper.Map<ProvinceDto>(province));
        }
    }
}
