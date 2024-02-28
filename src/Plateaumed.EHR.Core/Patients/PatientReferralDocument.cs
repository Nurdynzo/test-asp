using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Auditing;

namespace Plateaumed.EHR.Patients
{
    [Table("PatientReferralDocuments")]
    [Audited]
    public class PatientReferralDocument : FullAuditedEntity<long>
    {
        [StringLength(
            PatientReferralDocumentConsts.MaxReferringHealthCareProviderLength,
            MinimumLength = PatientReferralDocumentConsts.MinReferringHealthCareProviderLength
        )]
        public string ReferringHealthCareProvider { get; set; }

        [StringLength(
            PatientReferralDocumentConsts.MaxDiagnosisSummaryLength,
            MinimumLength = PatientReferralDocumentConsts.MinDiagnosisSummaryLength
        )]
        public string DiagnosisSummary { get; set; }

        //File
        public Guid? ReferralDocument { get; set; } //File, (BinaryObjectId)

        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; } = null;
    }
}
