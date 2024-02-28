using System.Collections.Generic;

namespace Plateaumed.EHR.Investigations.Dto
{
    public class RadiologyAndPulmonaryInvestigationDto
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }		
		public List<RadiologyAndPulmonaryInvestigationsDto> InvestigationComponents { get; set; }
	}
}

