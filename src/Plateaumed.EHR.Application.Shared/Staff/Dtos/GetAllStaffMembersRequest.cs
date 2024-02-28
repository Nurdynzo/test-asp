using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Plateaumed.EHR.Authorization.Users.Dto;
using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.Staff.Dtos
{
    public class GetAllStaffMembersRequest : PagedAndSortedResultRequestDto, IShouldNormalize, IGetUsersInput
    {
        public string Filter { get; set; }

        public string StaffCodeFilter { get; set; }

        public DateTime? MaxContractStartDateFilter { get; set; }
        public DateTime? MinContractStartDateFilter { get; set; }

        public DateTime? MaxContractEndDateFilter { get; set; }
        public DateTime? MinContractEndDateFilter { get; set; }

        public long? JobTitleIdFilter { get; set; }

        public long? JobLevelIdFilter { get; set; }

        public long? FacilityIdFilter { get; set; }

        public List<string> Permissions { get; set; }

        public int? Role { get; set; }

        public bool OnlyLockedUsers { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name,Surname";
            }

            Filter = Filter?.Trim();
        }
        
    }
}