using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public OutPatientListingType? OutPatientListingType { get; set; }
        
        public AppointmentStatusType? Status { get; set; }
    }
}