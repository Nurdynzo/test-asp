using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Authorization.Users;
using System.Collections.Generic;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class CreateOrEditPatientDto : EntityDto<long?>
    {
        [Required(ErrorMessage = "Gender is required")]
        public GenderType GenderType { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public TitleType? Title { get; set; }

        public string MiddleName { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        public string Address { get; set; }
        
        public bool IsNewToHospital { get; set; }

        public int? StateOfOriginId { get; set; }

        public int? CountryId { get; set; }

        public int? DistrictId { get; set; } 

        public long? UserId { get; set; }

        [Required]
        [StringLength(
            PatientConsts.MaxPatientCodeLength,
            MinimumLength = PatientConsts.MinPatientCodeLength
        )]
        public string PatientCode { get; set; }

        public string Ethnicity { get; set; }

        public Religion? Religion { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }

        public BloodGroup? BloodGroup { get; set; }

        public BloodGenotype? BloodGenotype { get; set; }

        public int NuclearFamilySize { get; set; }

        public int NumberOfSiblings { get; set; }

        public string PositionInFamily { get; set; }

        public int NumberOfChildren { get; set; }

        public int NumberOfSpouses { get; set; }


        public int NoOfMaleChildren { get; set; }


        public int NoOfFemaleChildren { get; set; }


        public int NoOfMaleSiblings { get; set; }

        public String ProfilePictureId { get; set; }

        public string ProfilePictureUrl { get; set; }

        public int NoOfFemaleSiblings { get; set; }

        public string IdentificationCode { get; set; }
        public Guid? ReferralDocument { get; set; }
        public IdentificationType? IdentificationType { get; set; }

        public ICollection<PatientOccupationDto> PatientOccupations { get; set; }

        public ICollection<CreateOrEditPatientRelationDto> Relations { get; set; }
    }
}
