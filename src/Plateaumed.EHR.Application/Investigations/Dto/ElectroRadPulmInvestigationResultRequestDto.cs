using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Plateaumed.EHR.Investigations.Dto
{
    public class ElectroRadPulmInvestigationResultRequestDto
	{
        public long PatientId { get; set; }

        public long InvestigationId { get; set; }

        public long InvestigationRequestId { get; set; }

        public DateOnly ResultDate { get; set; }

        public TimeOnly ResultTime { get; set; }

        public string Conclusion { get; set; }

        public long? EncounterId { get; set; }

        public long? ReviewerId { get; set; }

        public long? ProcedureId { get; set; }

        [Required]
        public List<IFormFile> ImageFiles { get; set; }
    }
}

