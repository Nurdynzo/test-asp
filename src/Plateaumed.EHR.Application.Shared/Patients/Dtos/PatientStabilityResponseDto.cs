using System;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class PatientStabilityResponseDto
	{
		public long Id { get; set; }
		public long PatientId { get; set; }
		public long CreatorUserId { get; set; }
		public PatientStabilityStatus Status { get; set; }
		public DateTime CreationTime { get; set; }
	}
}

