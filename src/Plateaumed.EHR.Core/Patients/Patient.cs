using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;
using Plateaumed.EHR.Authorization.Users;
using System.Collections.Generic;
using Abp.Extensions;
using Plateaumed.EHR.Misc.Country;
using Plateaumed.EHR.NurseCarePlans;
using Plateaumed.EHR.Vaccines;
using Plateaumed.EHR.Investigations;

namespace Plateaumed.EHR.Patients
{
    [Table("Patients")]
    [Audited]
    public class Patient : FullAuditedEntity<long>
    {
        [Required]
        public Guid UuId { get; set; }

        [Required]
        public GenderType GenderType { get; set; }

        [Required]
        [StringLength(UserConsts.MaxFirstNameLength,
            MinimumLength= UserConsts.MinFirstNameLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(UserConsts.MaxLastNameLength,
            MinimumLength = UserConsts.MinLastNameLength)]
        public string LastName { get; set; }

        [Required]
        [StringLength(UserConsts.MaxPhoneNumberLength,
            MinimumLength = UserConsts.MinPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        [StringLength(UserConsts.MaxEmailLength,
            MinimumLength = UserConsts.MinEmailLength)]
        public string EmailAddress { get; set; }

        [StringLength(UserConsts.MaxAddressLength, 
            MinimumLength = UserConsts.MinAddressLength)]
        public string Address { get; set; }

        public bool IsNewToHospital { get; set; } = false;

        public TitleType? Title { get; set; }

        [StringLength(UserConsts.MaxMiddleNameLength, 
            MinimumLength = UserConsts.MinMiddleNameLength)]
        public string MiddleName { get; set; }

        public int? DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public District District { get; set; } = null;

        public int? StateOfOriginId { get; set; }

        [ForeignKey("StateId")]
        public Region StateOfOriginFk { get; set; } = null;

        public int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country CountryFk { get; set; } = null;

        public long? UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserFk { get; set; } = null;

        [StringLength(
            PatientConsts.MaxEthnicityLength,
            MinimumLength = PatientConsts.MinEthnicityLength
        )]
        public string Ethnicity { get; set; }

        public Religion? Religion { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }

        public BloodGroup? BloodGroup { get; set; }

        public BloodGenotype? BloodGenotype { get; set; }
        
        public BloodGroupAndGenotypeSource? BloodGroupSource { get; set; }
        
        public BloodGroupAndGenotypeSource? GenotypeSource { get; set; }

        public int NuclearFamilySize { get; set; }

        public int NumberOfSiblings { get; set; }
        
       
        public int NoOfMaleChildren { get; set; }
    
        
        public int  NoOfFemaleChildren { get; set; }

        
        public int  NoOfMaleSiblings { get; set; }

        
        public int NoOfFemaleSiblings { get; set; }

        public string PositionInFamily { get; set; }

        public int NumberOfChildren { get; set; }

        public int NumberOfSpouses { get; set; }

        [StringLength(UserConsts.MaxIdentificationCodeLength, 
            MinimumLength = UserConsts.MinIdentificationCodeLength)]
        public string IdentificationCode { get; set; }

        public IdentificationType? IdentificationType { get; set; }

        public decimal WalletBalance { get; set; }

        public ICollection<PatientOccupation> PatientOccupations { get; set; }

        public ICollection<PatientRelation> Relations { get; set; }

        public ICollection<PatientInsurer> Insurers { get; set; }

        public ICollection<PatientReferralDocument> ReferralDocuments { get; set; }

        public ICollection<PatientAppointment> PatientAppointments { get; set; }
        
        public virtual ICollection<AllInputs.Symptom> Symptoms { get; set; }
        
        public virtual ICollection<AllInputs.BedMaking> BedMakings { get; set; }

        public virtual ICollection<AllInputs.Feeding> Feeding { get; set; }

        public virtual ICollection<AllInputs.PlanItems> PlanItems { get; set; }

        public virtual ICollection<AllInputs.InputNotes> InputNotes { get; set; }

        public virtual ICollection<AllInputs.WoundDressing> WoundDressing { get; set; }

        public virtual ICollection<AllInputs.Meals> Meals { get; set; }
        public virtual ICollection<Vaccination> Vaccinations { get; set; }
        
        public virtual ICollection<VaccinationHistory> VaccineHistories { get; set; }

        public ICollection<PatientCodeMapping> PatientCodeMappings { get; set; } = new List<PatientCodeMapping>();

        public ICollection<PatientEncounter> PatientEncounters { get; set; } = new List<PatientEncounter>();

        [NotMapped]
        public string FullName => FirstName + " " + LastName;
        
        [NotMapped]
        public string DisplayName => (Title.HasValue ? Title.GetValueOrDefault() + " " : string.Empty) + FirstName + " " + (!MiddleName.IsNullOrEmpty() ? MiddleName + " " : string.Empty) + LastName;
       
        [StringLength(PatientConsts.MaxPictureIdLength)]
        public string ProfilePictureId { get; set; }

        [StringLength(PatientConsts.MaxPictureUrlLength)]
        public string PictureUrl { get; set; }
    }
}
