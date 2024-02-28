using System;

namespace Plateaumed.EHR.Investigations.Dto
{
    public class ViewTestInfoResponse
	{
		public string PatientFirstName { get; set; }
		public string PatientLastName { get; set; }
		public string PatientAge { get; set; }
		public string Gender { get; set; }
		public string PatientImageUrl { get; set; }
		public string PatientCode { get; set; }

		public string RequestorFirstName { get; set; }
		public string RequestorLastName { get; set; }
		public string RequestorContactPhoneNumber { get; set; }
		public string RequestorUnit { get; set; }
		public string RequestorTitle { get; set; }
		public string RequestorImageUrl { get; set; }

		public string DiagnosisDescription { get; set; }
        public string DiagnosisNotes { get; set; }
        public string TestName { get; set; }
		public string Specimen { get; set; }
		public string Organism { get; set; }
		public string TestCategory { get; set; }
		public string TestStatus { get; set; }
		public string ClinicOrWard { get; set; }
		public string InvestigationRequestNotes { get; set; }
		public DateTime DateRequested { get; set; }
	}
}

