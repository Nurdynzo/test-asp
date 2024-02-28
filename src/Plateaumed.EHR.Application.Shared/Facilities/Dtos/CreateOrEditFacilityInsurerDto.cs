using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Insurance;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class CreateOrEditFacilityInsurerDto : EntityDto<long?>
    {
        public bool IsActive { get; set; }

        public long? FacilityGroupId { get; set; }

        public long? FacilityId { get; set; }

        public long InsuranceProviderId { get; set; }

        public string InsuranceProviderName { get; set; }

        public InsuranceProviderType? InsuranceProviderType { get; set; }
    }
}