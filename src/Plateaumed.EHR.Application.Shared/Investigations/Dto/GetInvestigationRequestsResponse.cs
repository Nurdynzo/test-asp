using System;

namespace Plateaumed.EHR.Investigations.Dto;

public class GetInvestigationRequestsResponse
{
    public long Id { get; set; }
    public long InvestigationId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Specimen { get; set; }
    public string SpecificOrganism { get; set; }
    public bool Urgent { get; set; }
    public bool WithContrast { get; set; }
    public DateTime CreationTime { get; set; }
    
    public long? ProcedureId { get; set; } = null;

}