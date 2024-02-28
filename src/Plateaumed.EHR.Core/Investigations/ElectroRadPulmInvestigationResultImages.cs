using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Investigations
{
    public class ElectroRadPulmInvestigationResultImages: FullAuditedEntity<long>, IMustHaveTenant
    {
        public long ElectroRadPulmInvestigationResultId { get; set; }

        [ForeignKey(nameof(ElectroRadPulmInvestigationResultId))]
        public ElectroRadPulmInvestigationResult ElectroRadPulmInvestigationResult { get; set; }

        [StringLength(PatientConsts.MaxPictureUrlLength)]
        public string ImageUrl { get; set; }

        public string FileId { get; set; }

        public string FileName { get; set; }

        public int TenantId { get; set; }
    }
}
