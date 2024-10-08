﻿using AutoMapper;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Startup
{
    public static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserDto>()
                .ForMember(dto => dto.Roles, options => options.Ignore())
                .ForMember(dto => dto.OrganizationUnits, options => options.Ignore());
        }
    }
}