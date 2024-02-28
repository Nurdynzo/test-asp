using System;
using System.Collections.Generic;
using System.Text;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetPatientAllergyResponseDto : CreatePatientAllergyCommandRequest
    {
        public long Id { get; set; }
        public long CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
