using System;
using Plateaumed.EHR.Misc;

namespace Plateaumed.EHR.Investigations.Dto
{
    public class GetInvestigationForPatientResponse
	{
        public long PatientId { get; set; }
        public InvestigationStatus? Status { get; set; }
        public string NameOfInvestigation { get; set; }
        public DateTime DateRequested { get; set; }
        public bool IsDeleted { get; set; }
    }
}
