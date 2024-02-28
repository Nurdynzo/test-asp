using System.Collections.Generic;

namespace Plateaumed.EHR.Investigations.Dto
{
    public class GetPatientInvestigationRequest
	{
        public List<long> InvestigationIds { get; set; }
        public long PatientId { get; set; }
    }
}

