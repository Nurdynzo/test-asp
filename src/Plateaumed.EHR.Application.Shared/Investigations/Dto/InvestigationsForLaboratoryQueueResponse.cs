using System.Collections.Generic;

namespace Plateaumed.EHR.Investigations.Dto
{
    public class InvestigationsForLaboratoryQueueResponse
    {
        public PatientDetail PatientDetail { get; set; }
		public List<InvestigationResponseList> InvestigationItems { get; set; }
	}
}

