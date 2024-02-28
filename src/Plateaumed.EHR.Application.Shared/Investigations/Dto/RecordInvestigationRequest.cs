using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.Investigations.Dto;

public class RecordInvestigationRequest
{
    public long PatientId { get; set; }
    public long InvestigationId { get; set; }
    public long InvestigationRequestId { get; set; }
    public string Name { get; set; }
    public string Reference { get; set; }
    public DateOnly SampleCollectionDate { get; set; }
    public DateOnly ResultDate { get; set; }
    public TimeOnly SampleTime { get; set; }
    public TimeOnly ResultTime { get; set; }
    public string Specimen { get; set; }
    public string Conclusion { get; set; }
    public string SpecificOrganism { get; set; }
    public string View { get; set; }
    public string Notes { get; set; }
    public long EncounterId { get; set; }

    public long? ReviewerId { get; set; }

    public long? ProcedureId { get; set; } = null;

    public List<InvestigationComponentResultDto> InvestigationComponentResults { get; set; }
}