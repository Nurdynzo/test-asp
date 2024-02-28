using System;
using System.Collections.Generic;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class ClinicalInvestigationResultResponse
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string Conclusion { get; set; }
        public List<ResultComponentResponse> ResultComponent { get; set; }
        public long? FacilityId { get; set; }

    }

}
