﻿using System;
using Abp.AutoMapper;
using Plateaumed.EHR.Authorization.Roles;

namespace Plateaumed.EHR.Dto
{
    [AutoMapFrom(typeof(Role))]
    public class RoleDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsStatic { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime LastModificationTime { get; set; }

        public long? CreatorUserId { get; set; }

        public long? LastModifierUserId { get; set; }

        public int? TenantId { get; set; }
    }
}