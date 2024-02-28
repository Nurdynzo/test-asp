namespace Plateaumed.EHR.PatientProfile.Dto;

public class GetClinicalInvestigationQuery
{
    public long PatientId { get; set; }
    public string CategoryFilter { get; set; }
    public string TestFilter { get; set; }
    public InvestigationResultDateFilter? DateFilter { get; set; }

}
