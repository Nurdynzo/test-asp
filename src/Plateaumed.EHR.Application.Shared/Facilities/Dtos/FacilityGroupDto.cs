using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.MultiTenancy;
using System.Collections.Generic;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class FacilityGroupDto : EntityDto<long>
    {
        public string Name { get; set; }

        public TenantCategoryType Category { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Website { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public List<FacilityDto> ChildFacilities {get; set;}

        public Guid? LogoId { get; set; }
    }
}
