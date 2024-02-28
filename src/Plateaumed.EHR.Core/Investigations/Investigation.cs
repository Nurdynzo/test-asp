using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Plateaumed.EHR.Investigations
{
    public class Investigation : Entity<long>
    {
        public long? PartOfId { get; set; }

        [ForeignKey(nameof(PartOfId))]
        public Investigation PartOf { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string SnomedId { get; set; }

        public string Synonyms { get; set; }

        public string Specimen { get; set; }

        public string SpecificOrganism { get; set; }

        public List<Investigation> Components { get; set; } = new();

        public List<InvestigationRange> Ranges { get; set; } = new();

        public List<InvestigationSuggestion> Suggestions { get; set; } = new();

        public List<InvestigationBinaryResult> Results { get; set; } = new();

        public List<DipstickInvestigation> Dipstick { get; set; } = new();

        public List<RadiologyAndPulmonaryInvestigation> RadiologyAndPulmonary { get; set; } = new();

        public MicrobiologyInvestigation Microbiology { get; set; } = new();
    }
}