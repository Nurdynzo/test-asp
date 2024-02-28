using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.Investigations.Dto
{
    public class ElectroRadPulmInvestigationResultResponseDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public long PatientId { get; set; }
        public long InvestigationId { get; set; }
        public long InvestigationRequestId { get; set; }
        public TimeOnly ResultTime { get; set; }
        public DateOnly ResultDate { get; set; }
        public string Conclusions { get; set; }
        public List<string> ResultImageUrls { get; set; }
        public DateTime CreationTime { get; set; }
        public long? ProcedureId { get; set; }
    }
}

