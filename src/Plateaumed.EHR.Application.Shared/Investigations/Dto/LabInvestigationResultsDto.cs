namespace Plateaumed.EHR.Investigations.Dto
{
    public class LabInvestigationResultsDto
	{
		public string Name { get; set; }
		public string Result { get; set; }
		public string Reference { get; set; }
		public decimal MinRange { get; set; }
		public decimal MaxRange { get; set; }
		public long? ProcedureId { get; set; } = null;
	}
}

