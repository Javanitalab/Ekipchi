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
using Hastnama.Ekipchi.Data.Event;
using Hastnama.Ekipchi.Data.Event.Gallery;
using Hastnama.Ekipchi.Data.Event.Schedule;
using Hastnama.Ekipchi.Data.Faq;
using Hastnama.Ekipchi.Data.Group;
using Hastnama.Ekipchi.Data.Host;
using Hastnama.Ekipchi.Data.Host.AvailableDate;
using Hastnama.Ekipchi.Data.Message;
using Hastnama.Ekipchi.Data.Province;
using Hastnama.Ekipchi.Data.Region;
using Hastnama.Ekipchi.Data.Role;
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
                .ForMember(x => x.UserInRoles,
                    opt => opt.MapFrom(des =>
                        des.UserInRoles.Select(ur => new RoleDto
                        {
                            Id = ur.Role.Id, Name = ur.Role.Name,
                            RolePermissions = ur.Role.RolePermissions.Select(rp => new RolePermissionDto
                                {
                                    Id = rp.Permission.Id, Name = rp.Permission.Name, ParentId = rp.Permission.ParentId
                                })
                                .ToList()
                        })))
                .ForMember(x => x.Mobile, opt => opt.MapFrom(des => des.Mobile));

            CreateMap<UserDto, User>()
                .ForMember(x => x.Status, opt => opt.MapFrom(des => des.Status))
                .ForMember(x => x.Username, opt => opt.MapFrom(des => des.Username))
                .ForMember(x => x.Email, opt => opt.MapFrom(des => des.Email))
                .ForMember(x => x.Gender, opt => opt.MapFrom(des => des.Gender))
                .ForMember(x => x.Avatar, opt => opt.MapFrom(des => des.Avatar))
                .ForMember(x => x.Id, opt => opt.MapFrom(des => des.Id))
                .ForMember(x => x.Mobile, opt => opt.MapFrom(des => des.Mobile));

            CreateMap<UpdateUserDto, User>()
                .ForMember(x => x.Status, opt => opt.MapFrom(des => des.Status))
                .ForMember(x => x.Username, opt => opt.MapFrom(des => des.Username))
                .ForMember(x => x.Email, opt => opt.MapFrom(des => des.Email))
                .ForMember(x => x.Gender, opt => opt.MapFrom(des => des.Gender))
                .ForMember(x => x.Avatar, opt => opt.MapFrom(des => des.Avatar))
                .ForMember(x => x.Id, opt => opt.MapFrom(des => des.Id))
                .ForMember(x => x.Mobile, opt => opt.MapFrom(des => des.Mobile));

            CreateMap<CreateUserDto, User>()
                .ForMember(x => x.Status, opt => opt.MapFrom(des => des.Status))
                .ForMember(x => x.Username, opt => opt.MapFrom(des => des.Username))
                .ForMember(x => x.Password, opt => opt.MapFrom(des => StringUtil.HashPass(des.Password)))
                .ForMember(x => x.Email, opt => opt.MapFrom(des => des.Email))
                .ForMember(x => x.Gender, opt => opt.MapFrom(des => des.Gender))
                .ForMember(x => x.Avatar, opt => opt.MapFrom(des => des.Avatar))
                .ForMember(x => x.Id, opt => Guid.NewGuid())
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

            CreateMap<BlogDto, Blog>();

            CreateMap<Blog, BlogDto>();

            CreateMap<UpdateBlogDto, Blog>();

            CreateMap<CreateBlogDto, Blog>();

            #endregion

            #region BlogCategory

            CreateMap<BlogCategoryDto, BlogCategory>();

            CreateMap<BlogCategory, BlogCategoryDto>();

            CreateMap<UpdateBlogCategoryDto, BlogCategory>();

            CreateMap<CreateBlogCategoryDto, BlogCategory>();

            #endregion

            #region Event

            CreateMap<CreateEventDto, Event>()
                .ForMember(x => x.EventGallery,
                    opt => opt.MapFrom(x => new EventGallery
                        {Image = x.EventGallery.Image, UserId = x.EventGallery.UserId}));

            CreateMap<UpdateEventDto, Event>();

            CreateMap<Event, EventDto>()
                .ForMember(x => x.UserInEvents,
                    opt => opt.MapFrom(x => x.UserInEvents.Select(ur=>ur.User)));
                


            CreateMap<EventSchedule, EventScheduleDto>();

            CreateMap<EventScheduleDto, EventSchedule>();

            CreateMap<EventGallery, EventGalleryDto>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.User.Id))
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.User.Username))
                .ForMember(x => x.UserAvatar, opt => opt.MapFrom(x => x.User.Avatar));

            CreateMap<EventGalleryDto, EventGallery>();
            CreateMap<UpdateEventGalleryDto, EventGallery>();
            CreateMap<EventGallery, UpdateEventGalleryDto>();

            #endregion

            #region Category

            CreateMap<CategoryDto, Category>();

            CreateMap<Category, CategoryDto>();


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

            #region Group

            CreateMap<GroupDto, Group>()
                .ForMember(x => x.Id, opt => opt.MapFrom(o => Guid.NewGuid()))
                .ForMember(x => x.OwnerId, opt => opt.MapFrom(o => o.Owner.Id));

            CreateMap<Group, GroupDto>()
                .ForMember(x => x.Owner, opt => opt.MapFrom(o => o.User))
                .ForMember(x => x.UsersInGroups,
                    opt => opt.MapFrom(o => o.UserInGroups.Select(ug => ug.User)));


            CreateMap<UpdateGroupDto, Group>()
                .ForMember(x => x.OwnerId, opt => opt.MapFrom(o => o.OwnerId));


            CreateMap<CreateGroupDto, Group>()
                .ForMember(x => x.OwnerId, opt => opt.MapFrom(o => o.OwnerId));

            #endregion

            #region Role

            CreateMap<RoleDto, Role>();

            CreateMap<Role, FaqDto>();

            CreateMap<UpdateRoleDto, Role>();

            CreateMap<CreateRoleDto, Role>();

            #endregion

            #region RolePermission

            CreateMap<RolePermission, RolePermissionDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(des => des.PermissionId))
                .ForMember(x => x.Name, opt => opt.MapFrom(des => des.Permission.Name))
                .ForMember(x => x.ParentId, opt => opt.MapFrom(des => des.Permission.ParentId));

            #endregion

            #region Message

            CreateMap<UserMessage, UserMessageDto>()
                .ForMember(x => x.ReceiverName, opt => opt.MapFrom(x => x.ReceiverUser.Email))
                .ForMember(x => x.SenderName, opt => opt.MapFrom(x => x.SenderUser.Email))
                .ForMember(x => x.Body, opt => opt.MapFrom(des => des.Message.Body))
                .ForMember(x => x.Title, opt => opt.MapFrom(des => des.Message.Title))
                .ForMember(x => x.ParentId, opt => opt.MapFrom(x => x.Message.ParentId))
                .ForMember(x => x.SendDate, opt => opt.MapFrom((src, dest, destMember, context) =>
                    PersianDateUtil.ChangeDateTime(src.SendDate, context.Items["lang"].ToString())));

            CreateMap<Message, MessageDto>();

            CreateMap<CreateMessageDto, MessageDto>();

            CreateMap<CreateMessageDto, Message>().ForMember(x => x.CreateDate, opt => opt.MapFrom(o => DateTime.Now));

            CreateMap<CreateReplyTo, Message>().ForMember(x => x.CreateDate, opt => opt.MapFrom(des => DateTime.Now));

            #endregion

            #region Host

            CreateMap<HostDto, Host>();

            CreateMap<Host, HostDto>()
                .ForMember(x => x.Galleries, opt => opt.MapFrom(x => x.HostGalleries.Select(g => g.Image)))
                .ForMember(x => x.Categories,
                    opt => opt.MapFrom(x => x.HostCategories.Select(c => new CategoryDto
                        {Id = c.Category.Id, IsDeleted = c.Category.IsDeleted, Name = c.Category.Name})));

            CreateMap<UpdateHostDto, Host>();

            CreateMap<CreateHostDto, Host>();

            CreateMap<HostAvailableDateDto, HostAvailableDate>();

            CreateMap<HostAvailableDate, HostAvailableDateDto>();

            #endregion
        }
    }
}