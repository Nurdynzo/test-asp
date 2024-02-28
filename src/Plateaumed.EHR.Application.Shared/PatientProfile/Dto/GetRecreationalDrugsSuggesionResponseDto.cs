using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetRecreationalDrugsSuggesionResponseDto : EntityDto<long>
    {
        public string Name { get; set; }
        public long SnomedId { get; set; }
    }
}
