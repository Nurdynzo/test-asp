using Plateaumed.EHR.Facilities;
using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class FacilityDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Website { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public FacilityLevel? Level { get; set; }

        public string FacilityGroup { get; set; }

        public string FacilityType { get; set; }

        public bool? HasPharmacy { get; set; }

        public bool? HasLaboratory { get; set; }

        public Guid? LogoId { get; set; }
    }
}
