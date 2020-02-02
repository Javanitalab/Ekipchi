﻿using System;
using System.Linq;
using AutoMapper;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Common.Util;
using Hastnama.Ekipchi.Data.Auth;
using Hastnama.Ekipchi.Data.City;
using Hastnama.Ekipchi.Data.Country;
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
        }
    }
}