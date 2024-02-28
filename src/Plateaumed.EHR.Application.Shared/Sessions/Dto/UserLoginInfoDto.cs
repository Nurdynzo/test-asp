using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Sessions.Dto
{
    public class UserLoginInfoDto : EntityDto<long>
    {
        public TitleType? Title { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string FullName { get; set; }

        public string DisplayName { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string AltEmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePictureId { get; set; }

        public int? Age { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public GenderType? Gender { get; set; }

        public StaffLoginInfoDto StaffInfo { get; set; }

        public PatientLoginInfoDto PatientInfo { get; set; }
    }
}
