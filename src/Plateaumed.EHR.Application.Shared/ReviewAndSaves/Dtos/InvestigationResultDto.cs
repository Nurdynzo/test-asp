using System;

namespace Plateaumed.EHR.ReviewAndSaves.Dtos
{
    public class InvestigationResultDto
    {
        public long Id { get; set; }
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
    }
}
