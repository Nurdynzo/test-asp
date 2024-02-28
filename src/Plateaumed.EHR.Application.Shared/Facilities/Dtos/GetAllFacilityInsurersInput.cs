using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetAllFacilityInsurersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int? IsActiveFilter { get; set; }

        public string FacilityGroupNameFilter { get; set; }

        public string FacilityNameFilter { get; set; }

        public string InsuranceProviderNameFilter { get; set; }

    }
}