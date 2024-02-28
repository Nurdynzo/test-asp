using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.MultiTenancy.Dto
{
    public class RegisterTenantOutput
    {
        public int TenantId { get; set; }

        public string TenancyName { get; set; }

        public TenantCategoryType Category { get; set; }

        public TenantType Type { get; set; }

        public string IndividualSpecialization { get; set; }

        public string IndividualGraduatingSchool { get; set; }

        public string IndividualGraduatingYear { get; set; }

        public bool HasSignedAgreement { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public bool IsTenantActive { get; set; }

        public bool IsEmailConfirmationRequired { get; set; }
    }
}