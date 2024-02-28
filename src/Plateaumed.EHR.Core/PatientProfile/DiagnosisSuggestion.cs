using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("DiagnosisSuggestions")]
    public class DiagnosisSuggestion : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public long? SnomedId { get; set; }
    }
}
