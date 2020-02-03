﻿using System;
using System.Linq;
using AutoMapper;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Common.Util;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.Data.Blog;
using Hastnama.Ekipchi.Data.BlogCategory;
using Hastnama.Ekipchi.Data.Category;
using Hastnama.Ekipchi.Data.City;
using Hastnama.Ekipchi.Data.Comment;
using Hastnama.Ekipchi.Data.Country;
using Hastnama.Ekipchi.Data.Coupon;
using Hastnama.Ekipchi.Data.Faq;
using Hastnama.Ekipchi.Data.Host;
using Hastnama.Ekipchi.Data.Province;
using Hastnama.Ekipchi.Data.Region;
using Hastnama.Ekipchi.Data.User;
using Hastnama.Ekipchi.DataAccess.Entities;

namespace Hastnama.Ekipchi.Api.Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User

            CreateMap<RegisterDto, User>()
                .ForMember(x => x.CreateDateTime, opt => opt.MapFrom(o => DateTime.Now))
                .ForMember(x => x.Password, opt => opt.MapFrom(o => StringUtil.HashPass(o.Password)))
                .ForMember(x => x.Status, opt => opt.MapFrom(des => UserStatus.Active))
                .ForMember(x => x.Username, opt => opt.MapFrom(des => des.Username))
                .ForMember(x => x.Email, opt => opt.MapFrom(des => des.Email))
                .ForMember(x => x.Mobile, opt => opt.MapFrom(des => des.Mobile));

            CreateMap<User, UserDto>()
                .ForMember(x => x.Status, opt => opt.MapFrom(des => UserStatus.Active))
                .ForMember(x => x.Username, opt => opt.MapFrom(des => des.Username))
                .ForMember(x => x.Email, opt => opt.MapFrom(des => des.Email))
                .ForMember(x => x.Gender, opt => opt.MapFrom(des => des.Gender))
                .ForMember(x => x.Avatar, opt => opt.MapFrom(des => des.Avatar))
                .ForMember(x => x.Id, opt => opt.MapFrom(des => des.Id))
                .ForMember(x => x.Role, opt => opt.MapFrom(des => des.Role))
                .ForMember(x => x.Mobile, opt => opt.MapFrom(des => des.Mobile));

            CreateMap<UserDto, User>()
                .ForMember(x => x.Status, opt => opt.MapFrom(des => des.Status))
                .ForMember(x => x.Username, opt => opt.MapFrom(des => des.Username))
                .ForMember(x => x.Email, opt => opt.MapFrom(des => des.Email))
                .ForMember(x => x.Gender, opt => opt.MapFrom(des => des.Gender))
                .ForMember(x => x.Avatar, opt => opt.MapFrom(des => des.Avatar))
                .ForMember(x => x.Id, opt => opt.MapFrom(des => des.Id))
                .ForMember(x => x.Role, opt => opt.MapFrom(des => des.Role))
                .ForMember(x => x.Mobile, opt => opt.MapFrom(des => des.Mobile));

            CreateMap<UpdateUserDto, User>()
                .ForMember(x => x.Status, opt => opt.MapFrom(des => des.Status))
                .ForMember(x => x.Username, opt => opt.MapFrom(des => des.Username))
                .ForMember(x => x.Email, opt => opt.MapFrom(des => des.Email))
                .ForMember(x => x.Gender, opt => opt.MapFrom(des => des.Gender))
                .ForMember(x => x.Avatar, opt => opt.MapFrom(des => des.Avatar))
                .ForMember(x => x.Id, opt => opt.MapFrom(des => des.Id))
                .ForMember(x => x.Role, opt => opt.MapFrom(des => des.Role))
                .ForMember(x => x.Mobile, opt => opt.MapFrom(des => des.Mobile));

            CreateMap<CreateUserDto, User>()
                .ForMember(x => x.Status, opt => opt.MapFrom(des => des.Status))
                .ForMember(x => x.Username, opt => opt.MapFrom(des => des.Username))
                .ForMember(x => x.Password, opt => opt.MapFrom(des => StringUtil.HashPass(des.Password)))
                .ForMember(x => x.Email, opt => opt.MapFrom(des => des.Email))
                .ForMember(x => x.Gender, opt => opt.MapFrom(des => des.Gender))
                .ForMember(x => x.Avatar, opt => opt.MapFrom(des => des.Avatar))
                .ForMember(x => x.Id, opt => Guid.NewGuid())
                .ForMember(x => x.Role, opt => opt.MapFrom(des => des.Role))
                .ForMember(x => x.Mobile, opt => opt.MapFrom(des => des.Mobile));

            #endregion

            #region City

            CreateMap<CityDto, City>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.CountyId, opt => opt.MapFrom(o => o.CountyId))
                .ForMember(x => x.Regions, opt => opt.MapFrom(o => o.Regions.Select(r => new Region {Id = r.Id})))
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id));

            CreateMap<City, CityDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.CountyName, opt => opt.MapFrom(o => o.County.Name))
                .ForMember(x => x.CountyId, opt => opt.MapFrom(o => o.County.Id))
                .ForMember(x => x.Regions, opt => opt.MapFrom(o => o.Regions.Select(r => new Region {Id = r.Id})))
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id));

            CreateMap<UpdateCityDto, City>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.CountyId, opt => opt.MapFrom(o => o.CountyId))
                .ForMember(x => x.Regions, opt => opt.MapFrom(o => o.Regions.Select(r => new Region {Id = r.Id})))
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id));

            CreateMap<City, UpdateCityDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.CountyName, opt => opt.MapFrom(o => o.County.Name))
                .ForMember(x => x.CountyId, opt => opt.MapFrom(o => o.County.Id))
                .ForMember(x => x.Regions, opt => opt.MapFrom(o => o.Regions.Select(r => new RegionDto {Id = r.Id})))
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id));

            CreateMap<CreateCityDto, City>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.CountyId, opt => opt.MapFrom(o => o.CountyId));

            #endregion

            #region County

            CreateMap<CountyDto, County>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.ProvinceId, opt => opt.MapFrom(o => o.ProvinceId))
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id));

            CreateMap<County, CountyDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.ProvinceId, opt => opt.MapFrom(o => o.Province.Id))
                .ForMember(x => x.ProvinceName, opt => opt.MapFrom(o => o.Province.Name))
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id));

            CreateMap<UpdateCountyDto, County>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.ProvinceId, opt => opt.MapFrom(o => o.ProvinceId))
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id));

            CreateMap<CreateCountyDto, County>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.ProvinceId, opt => opt.MapFrom(o => o.ProvinceId));

            #endregion

            #region Province

            CreateMap<ProvinceDto, Province>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.Counties, opt => opt.MapFrom(o => o.Counties.Select(c => new County {Id = c.Id})))
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id));

            CreateMap<Province, ProvinceDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.Counties,
                    opt => opt.MapFrom(o => o.Counties.Select(c => new CountyDto
                        {Id = c.Id, Name = c.Name, ProvinceId = c.Province.Id, ProvinceName = c.Province.Name})))
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id));

            CreateMap<UpdateProvinceDto, Province>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.Counties, opt => opt.MapFrom(o => o.Counties.Select(c => new County {Id = c.Id})))
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id));

            CreateMap<CreateProvinceDto, Province>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name));

            #endregion

            #region Region

            CreateMap<RegionDto, Region>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
                .ForMember(x => x.CityId, opt => opt.MapFrom(o => o.CityId))
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id));

            CreateMap<Region, RegionDto>()
                .ForMember(x => x.CityId, opt => opt.MapFrom(o => o.City.Id))
                .ForMember(x => x.CityName, opt => opt.MapFrom(o => o.City.Name));

            CreateMap<UpdateRegionDto, Region>()
                .ForMember(x => x.CityId, opt => opt.MapFrom(o => o.CityId));

            CreateMap<CreateRegionDto, Region>()
                .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name));

            #endregion

            #region Blog

            CreateMap<BlogDto, Blog>()
                .ForMember(x => x.UseId, opt => opt.MapFrom(o => o.User.Id))
                .ForMember(x => x.BlogCategoryId, opt => opt.MapFrom(o => o.BlogCategoryId));

            CreateMap<Blog, BlogDto>()
                .ForMember(x => x.User,
                    opt => opt.MapFrom(o => new UserDto {Id = o.User.Id, Username = o.User.Username}))
                .ForMember(x => x.BlogCategoryId, opt => opt.MapFrom(o => o.BlogCategory.Id))
                .ForMember(x => x.BlogCategoryName, opt => opt.MapFrom(o => o.BlogCategory.Name));

            CreateMap<UpdateBlogDto, Blog>()
                .ForMember(x => x.BlogCategoryId, opt => opt.MapFrom(o => o.BlogCategoryId));

            CreateMap<CreateBlogDto, Blog>()
                .ForMember(x => x.BlogCategoryId, opt => opt.MapFrom(o => o.BlogCategoryId))
                .ForMember(x => x.UseId, opt => opt.MapFrom(o => o.UserId));

            #endregion

            #region BlogCategory

            CreateMap<BlogCategoryDto, BlogCategory>();

            CreateMap<BlogCategory, BlogCategoryDto>();

            CreateMap<UpdateBlogCategoryDto, BlogCategory>();

            CreateMap<CreateBlogCategoryDto, BlogCategory>();

            #endregion
            
            #region Category

            CreateMap<CategoryDto, Category>()
                .ForMember(x => x.HostCategories,
                    opt => opt.MapFrom(o => o.Hosts.Select(h => new HostCategory {CategoryId = o.Id, HostId = h.Id})));

            CreateMap<Category, CategoryDto>()
                .ForMember(x => x.Hosts,
                    opt => opt.MapFrom(o =>
                        o.HostCategories.Select(h => new HostDto {Id = h.Host.Id, Name = h.Host.Name})));


            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<CreateCategoryDto, Category>();

            #endregion

            #region Comment

            CreateMap<CommentDto, Comment>();

            CreateMap<Comment, CommentDto>();

            CreateMap<UpdateCommentDto, Comment>();

            CreateMap<CreateCommentDto, Comment>();

            #endregion

            #region Coupon

            CreateMap<CouponDto, Coupon>();

            CreateMap<Coupon, CouponDto>();

            CreateMap<UpdateCouponDto, Coupon>();

            CreateMap<CreateCouponDto, Coupon>();

            #endregion
            
            #region Faq

            CreateMap<FaqDto, Faq>();

            CreateMap<Faq, FaqDto>();

            CreateMap<UpdateFaqDto, Faq>();

            CreateMap<CreateFaqDto, Faq>();

            #endregion
        }
    }
}