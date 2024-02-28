using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.PhysicalExaminations
{
    public class PhysicalExamination : Entity<long>
    {
        public string Type { get; set; }
        public long? PhysicalExaminationTypeId { get; set; }
        [ForeignKey("PhysicalExaminationTypeId")]
        public virtual PhysicalExaminationType PhysicalExaminationType { get; set; }
        public string Header { get; set; }
        public string PresentTerms { get; set; }
        public string SnomedId { get; set; }
        public string AbsentTerms { get; set; }
        public string AbsentTermsSnomedId { get; set; }
        public bool Site { get; set; }
        public bool Plane { get; set; }
        public string Qualifiers { get; set; }
        public GenderType? Gender { get; set; }
        public string Unit { get; set; }
    }
}
