using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class PatientStabilityRequestDto
	{
        public long PatientId { get; set; }      

        public long EncounterId { get; set; }

        public PatientStabilityStatus Status { get; set; }
    }
}

