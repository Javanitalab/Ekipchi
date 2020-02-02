using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Region;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Hastnama.GuitarIranShop.DataAccess.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class RegionService : Repository<EkipchiDbContext, Region>, IRegionService
    {
        private readonly IMapper _mapper;

        public RegionService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<RegionDto>>> List(PagingOptions pagingOptions,
            FilterRegionQueryDto filterQueryDto)
        {
            var counties = await WhereAsyncAsNoTracking(c =>
                    (string.IsNullOrEmpty(filterQueryDto.Name) ||
                     c.Name.ToLower().Contains(filterQueryDto.Name.ToLower())),
                pagingOptions, c => c.City);


            return Result<PagedList<RegionDto>>.SuccessFull(counties.MapTo<RegionDto>(_mapper));
        }

        public async Task<Result> Update(UpdateRegionDto updateRegionDto)
        {
            var duplicateRegion = await FirstOrDefaultAsyncAsNoTracking(c => c.Name == updateRegionDto.Name);
            if (duplicateRegion != null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateRegionName}));

            var region = await FirstOrDefaultAsync(c => c.Id == updateRegionDto.Id);
            _mapper.Map(updateRegionDto, region);
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<RegionDto>> Create(CreateRegionDto createRegionDto)
        {
            var duplicateRegion = await FirstOrDefaultAsyncAsNoTracking(c => c.Name == createRegionDto.Name);
            if (duplicateRegion != null)
                return Result<RegionDto>.Failed(new BadRequestObjectResult(new ApiMessage
                    {Message = PersianErrorMessage.DuplicateRegionName}));

            var region = _mapper.Map(createRegionDto, new Region());
            await AddAsync(region);
            await Context.SaveChangesAsync();

            return Result<RegionDto>.SuccessFull(_mapper.Map<RegionDto>(region));
        }

        public async Task<Result<RegionDto>> Get(int id)
        {
            var region = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id, c => c.City);
            if (region == null)
                return Result<RegionDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.RegionNotFound}));

            return Result<RegionDto>.SuccessFull(_mapper.Map<RegionDto>(region));
        }
    }
}