using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class PatientCodeTemplateDto : EntityDto<long>
    {
        public string Prefix { get; set; }

        public int Length { get; set; }

        public string Suffix { get; set; }

        public int StartingIndex { get; set; }
    }
}
