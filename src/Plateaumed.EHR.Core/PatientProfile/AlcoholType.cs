using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Misc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("AlcoholTypes")]
    public class AlcoholType : FullAuditedEntity<long>
    {
        [StringLength(GeneralStringLengthConstant.MaxStringLength)]
        public string Type { get; set; }
        public float AlcoholUnit { get; set; }
    }
}
