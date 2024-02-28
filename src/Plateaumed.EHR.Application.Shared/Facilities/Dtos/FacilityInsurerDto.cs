using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class FacilityInsurerDto : EntityDto<long>
    {
        public bool IsActive { get; set; }

        public long? FacilityGroupId { get; set; }

        public long? FacilityId { get; set; }

        public long InsuranceProviderId { get; set; }

    }
}