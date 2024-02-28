using Plateaumed.EHR.Authorization.Users;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class CreateOrEditPatientRelationDto : EntityDto<long?>
    {
        [Required(ErrorMessage = "Relationship is required")]
        public Relationship Relationship { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        public TitleType Title { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "IsGuardian field is required")]
        public bool IsGuardian { get; set; }

        public string IdentificationCode { get; set; }

        public IdentificationType? IdentificationType { get; set; }

        public bool IsAlive { get; set; }

        public int AgeAtDeath { get; set; }

        public int AgeAtDiagnosis { get; set; }

        public ICollection<CreateOrEditPatientRelationDiagnosisDto> Diagnoses { get; set; }
    }
}
