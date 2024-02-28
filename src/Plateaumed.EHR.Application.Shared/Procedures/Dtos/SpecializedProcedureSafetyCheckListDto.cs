using System.Collections.Generic;
namespace Plateaumed.EHR.Procedures.Dtos
{
    public class SpecializedProcedureSafetyCheckListDto
    {
        public List<SafetyCheckList> CheckLists { get; set; }
        public long ProcedureId { get; set; }
        public long PatientId { get; set; }
    }

}
