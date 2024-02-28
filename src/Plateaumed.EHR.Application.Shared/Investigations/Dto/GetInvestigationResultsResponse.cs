using System.Collections.Generic;
using System;

namespace Plateaumed.EHR.Investigations.Dto;

public class GetInvestigationResultsResponse
{
    public long PatientId { get; set; }
    public long InvestigationId { get; set; }
    public long InvestigationRequestId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
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
    public DateTime CreationTime { get; set; }
    public long? ProcedureId { get; set; } = null;
    public bool IsDeleted { get; set; }
    public string DeletedUser { get; set; }

    public List<InvestigationComponentResultDto> InvestigationComponentResults { get; set; }
}
