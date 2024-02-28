using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Plateaumed.EHR.Authorization.Users.Dto
{
    public class UserListDto : EntityDto<long>, IPassivable, IHasCreationTime
    {
        public TitleType? Title { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string DisplayName { get; set; }

        public GenderType? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string IdentificationCode { get; set; }

        public IdentificationType? IdentificationType { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string State { get; set; }

        public string PostCode { get; set; }

        public string Country { get; set; }

        public string EmailAddress { get; set; }

        public string AltEmailAddress { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public string PhoneNumber { get; set; }

        public Guid? ProfilePictureId { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public List<UserListRoleDto> Roles { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }
    }
}