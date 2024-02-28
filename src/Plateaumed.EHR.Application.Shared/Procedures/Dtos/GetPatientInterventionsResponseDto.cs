using System.Collections.Generic;

namespace Plateaumed.EHR.Procedures.Dtos
{
    public class GetPatientInterventionsResponseDto
	{
        public long PatientId { get; set; }
        public string PictureUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string PatientCode { get; set; }
        public List<SelectedProceduresDto> Interventions { get; set; }
        public bool IsDeleted { get; set; }
    }
}
