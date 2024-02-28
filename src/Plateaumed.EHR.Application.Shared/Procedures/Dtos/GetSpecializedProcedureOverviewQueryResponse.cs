using System.Collections.Generic;
namespace Plateaumed.EHR.Procedures.Dtos
{
    public class GetSpecializedProcedureOverviewQueryResponse
    {
        public List<SpecializedProcedureDetails> Procedure { get; set; }
        public string Specialist { get; set; }
        public string SpecialistAssistant { get; set; }
        public string ScrubNurse { get; set; }
        public string BloodUnits { get; set; }
        public AdmissionDetails AdmissionDetails { get; set; }

    }

}
