using Abp.Application.Services.Dto;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Investigations.Dto
{
    public class GetInvestigationsForLaboratoryQueueRequest : PagedResultRequestDto
    {
        public string PatientName { get; set; }
        public string OrderBy { get; set; }
        public string InvestigationCategory { get; set; }
        public string Status { get; set; }
    }
}

