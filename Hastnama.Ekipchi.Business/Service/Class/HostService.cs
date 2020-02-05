using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Host;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class HostService : Repository<EkipchiDbContext, Host>, IHostService
    {
        private readonly IMapper _mapper;

        public HostService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<HostDto>>> List(PagingOptions pagingOptions,
            FilterHostQueryDto filterQueryDto)
        {
            var hosts = await WhereAsyncAsNoTracking(c =>
                    (string.IsNullOrEmpty(filterQueryDto.Name) ||
                     c.Name.ToLower().Contains(filterQueryDto.Name.ToLower())) && c.IsDeleted == false, pagingOptions,
                g => g.HostGalleries,
                g => g.HostCategories.Select(hc => hc.Category),
                g => g.HostAvailableDates);


            return Result<PagedList<HostDto>>.SuccessFull(hosts.MapTo<HostDto>(_mapper));
        }

        public async Task<Result> Update(UpdateHostDto updateHostDto)
        {
            var categories = new List<HostCategory>();
            if (updateHostDto.Categories != null && updateHostDto.Categories.Any())
            {
                categories = await Context.HostCategories.Where(c =>
                        updateHostDto.Categories.Contains(c.CategoryId) && c.HostId == updateHostDto.Id)
                    .ToListAsync();
                if (categories.Count != updateHostDto.Categories.Count)
                    return Result.Failed(new NotFoundObjectResult(
                        new ApiMessage
                            {Message = PersianErrorMessage.CategoryNotFound}));
            }

            var host = await FirstOrDefaultAsync(c => c.Id == updateHostDto.Id, g => g.HostGalleries,
                g => g.HostCategories,
                g => g.HostAvailableDates);


            _mapper.Map(updateHostDto, host);
            Context.HostGalleries.RemoveRange(host.HostGalleries);

            host.HostCategories = categories;
            host.HostGalleries = updateHostDto.Galleries.Select(g => new HostGallery {Image = g, Host = host}).ToList();

            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }

        public async Task<Result<HostDto>> Create(CreateHostDto createHostDto)
        {
            var categories = new List<Category>();
            if (createHostDto.Categories != null && createHostDto.Categories.Any())
            {
                categories = await Context.Categories.Where(c => createHostDto.Categories.Contains(c.Id))
                    .ToListAsync();
                if (categories.Count != createHostDto.Categories.Count())
                    return Result<HostDto>.Failed(new NotFoundObjectResult(
                        new ApiMessage
                            {Message = PersianErrorMessage.CategoryNotFound}));
            }


            var host = _mapper.Map<Host>(createHostDto);
            host.EventCount = 0;
            host.Id = Guid.NewGuid();
            host.HostCategories = categories.Select(c => new HostCategory {Category = c, Host = host}).ToList();
            host.HostGalleries = createHostDto.Galleries.Select(image => new HostGallery
            {
                Image = image,
                Host = host
            }).ToList();

            await AddAsync(host);
            await Context.SaveChangesAsync();

            return Result<HostDto>.SuccessFull(_mapper.Map<HostDto>(host));
        }

        public async Task<Result<HostDto>> Get(Guid id)
        {
            var host = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id && c.IsDeleted == false,
                g => g.HostGalleries,
                g => g.HostCategories.Select(hc => hc.Category),
                g => g.HostAvailableDates);
            if (host == null)
                return Result<HostDto>.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.HostNotFound}));

            return Result<HostDto>.SuccessFull(_mapper.Map<HostDto>(host));
        }

        public async Task<Result> Delete(Guid id)
        {
            var host = await FirstOrDefaultAsync(c => c.Id == id);
            if (host == null)
                return Result.Failed(new NotFoundObjectResult(
                    new ApiMessage
                        {Message = PersianErrorMessage.HostNotFound}));

            host.IsDeleted = true;
            await Context.SaveChangesAsync();

            return Result.SuccessFull();
        }
    }
}