using System;

namespace Plateaumed.EHR.Diagnoses.Dto
{
    public class PatientDiagnosisReturnDto
    {
        public long Id { get ; set ; }
        public int TenantId { get; set; }
        public long PatientId { get; set; }
        public long Sctid { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
    }
}
