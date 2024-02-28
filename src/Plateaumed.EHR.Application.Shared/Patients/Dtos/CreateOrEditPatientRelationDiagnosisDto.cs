using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class CreateOrEditPatientRelationDiagnosisDto : EntityDto<long?>
    {
        public string SctId { get; set; }

        [Required(ErrorMessage = "Name of diagnosis is required")]
        public string Name { get; set; }

        public bool IsCauseOfDeath { get; set; }
    }
}
