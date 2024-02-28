using Plateaumed.EHR.Patients;

using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class PatientDto : EntityDto<long>
    {
        public string PatientCode { get; set; }

        public string Address { get; set; }

        public string District { get; set; }

        public string Ethnicity { get; set; }

        public Religion? Religion { get; set; }

        public MaritalStatus? MaritalStatus { get; set; }

        public BloodGroup? BloodGroup { get; set; }

        public BloodGenotype? BloodGenotype { get; set; }

        public int? CountryId { get; set; }

        public long? PatientOccupationId { get; set; }

        public long? PatientOccupationCategoryId { get; set; }

        public decimal WalletBalance { get; set; }
    }
}
