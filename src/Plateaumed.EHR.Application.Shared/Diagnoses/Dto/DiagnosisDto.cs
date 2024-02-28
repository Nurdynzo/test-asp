using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Diagnoses.Dto
{
    public class DiagnosisDto
    {
        public int TenantId { get; set; }
        public long PatientId { get; set; }

        public long Sctid { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public int? Status { get; set; }

        public long? EncounterId { get; set; }

        public DateTime CreatedAt { get; set; }
        public long CreatorUserId { get; set; }
    }
}
