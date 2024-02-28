using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class GetAllFacilityDocumentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public long? FacilityIdFilter { get; set; }

        public long? FacilityGroupIdFilter { get; set; }
    }
}
