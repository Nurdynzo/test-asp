using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetAlcoholTypesResponseDto : EntityDto<long>
    {
        public string Type { get; set; }
        public float AlcoholUnit { get; set; }
    }
}
