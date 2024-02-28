using Plateaumed.EHR.Authorization.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;
using System.ComponentModel;
using System.Collections.Generic;

namespace Plateaumed.EHR.Patients
{
    [Table("PatientRelations")]
    [Audited]
    public class PatientRelation : FullAuditedEntity<long>
    {

        [Required]
        public virtual Relationship Relationship { get; set; }

        [Required]
        [StringLength(UserConsts.MaxFirstNameLength,
            MinimumLength = UserConsts.MinFirstNameLength)]
        public virtual string FirstName { get; set; }

        [StringLength(UserConsts.MaxMiddleNameLength,
            MinimumLength = UserConsts.MinMiddleNameLength)]
        public virtual string MiddleName { get; set; }

        [Required]
        [StringLength(UserConsts.MaxLastNameLength,
            MinimumLength = UserConsts.MinLastNameLength)]
        public virtual string LastName { get; set; }

        [Required]
        [StringLength(UserConsts.MaxPhoneNumberLength,
            MinimumLength = UserConsts.MinPhoneNumberLength)]
        public virtual string PhoneNumber { get; set; }

        public virtual TitleType Title { get; set; }

        [StringLength(UserConsts.MaxAddressLength,
            MinimumLength = UserConsts.MinAddressLength)]
        public virtual string Address { get; set; }

        [EmailAddress]
        [StringLength(UserConsts.MaxEmailLength,
            MinimumLength = UserConsts.MinEmailLength)]
        public virtual string Email { get; set; }

        public virtual bool IsGuardian { get; set; }

        public virtual long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; }

        [StringLength(UserConsts.MaxIdentificationCodeLength,
            MinimumLength = UserConsts.MinIdentificationCodeLength)]
        public string IdentificationCode { get; set; }

        public IdentificationType? IdentificationType { get; set; }

        [DefaultValue(true)]
        public virtual bool IsAlive { get; set; } = true;

        public virtual int AgeAtDeath { get; set; }

        public virtual int AgeAtDiagnosis { get; set; }

        public ICollection<PatientRelationDiagnosis> Diagnoses { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
