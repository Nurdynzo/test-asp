using System.Collections.Generic;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientsMedicationsResponse
	{
		public long PatientId { get; set; }
		public string PictureUrl { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string Gender { get; set; }
		public string Age { get; set; }
		public string PatientCode { get; set; }
		public List<MedicationsListDto> PatientMedications { get; set; }
	}
}

