using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class FacilityStaffDto : EntityDto<long>
    {

        public long FacilityId { get; set; }

        public long StaffMemberId { get; set; }
        public long? JobTitleId { get; set; }
        public string StaffCode { get; set; }
        public TitleType? Title { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }

    }
}