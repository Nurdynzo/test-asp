using System;
namespace Plateaumed.EHR.Investigations.Dto
{
	public class RecordInvestigationSampleRequest
	{
        public long PatientId { get; set; }
        public long InvestigationId { get; set; }
        public long InvestigationRequestId { get; set; }
        public long EncounterId { get; set; }
        public string NameOfInvestigation { get; set; }
        public DateOnly SampleCollectionDate { get; set; }       
        public TimeOnly SampleCollectionTime { get; set; }       
        public string Specimen { get; set; }
        
        public long? ProcedureId { get; set; } = null;

    }
}

