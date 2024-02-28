using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetPatientReviewOfSystemsDataResponseDto : EntityDto<long>
    {
        public string Name { get; set; }

        public long SnomedId { get; set; }

        public string Category { get; set; }

        public long PatientId { get; set; }
    }
}
