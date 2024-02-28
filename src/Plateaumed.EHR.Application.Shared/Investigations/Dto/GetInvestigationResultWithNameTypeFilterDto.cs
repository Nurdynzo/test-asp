namespace Plateaumed.EHR.Investigations.Dto
{
    public class GetInvestigationResultWithNameTypeFilterDto
	{
        public long PatientId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long? ProcedureId { get; set; } = null;
    }
}

