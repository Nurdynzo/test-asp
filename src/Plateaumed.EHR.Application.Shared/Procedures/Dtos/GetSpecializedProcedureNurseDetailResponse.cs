using System;
namespace Plateaumed.EHR.Procedures.Dtos
{
    public class GetSpecializedProcedureNurseDetailResponse
    {
        public long Id { get; set; }
        public string ScrubNurseName { get; set; }
        public string CirculatingNurseName { get; set; }
        public TimeOnly TimePatientReceived { get; set; }
        public long ProcedureId { get; set; }
    }
}
