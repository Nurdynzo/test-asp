using Plateaumed.EHR.Staff;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Plateaumed.EHR.Organizations;

namespace Plateaumed.EHR.Patients
{
    [Table("PatientAppointments")]
    [Audited]
    public class PatientAppointment : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [StringLength(PatientAppointmentConsts.MaxTitleLength, MinimumLength = PatientAppointmentConsts.MinTitleLength)]
        public virtual string Title { get; set; }

        public virtual int Duration { get; set; }

        public virtual DateTime StartTime { get; set; }

        public virtual bool IsRepeat { get; set; }

        [StringLength(PatientAppointmentConsts.MaxNotesLength, MinimumLength = PatientAppointmentConsts.MinNotesLength)]
        public virtual string Notes { get; set; }

        public virtual AppointmentRepeatType RepeatType { get; set; }

        public virtual AppointmentStatusType Status { get; set; }

        public virtual AppointmentType Type { get; set; }

        public virtual long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient PatientFk { get; set; }

        public virtual long? PatientReferralDocumentId { get; set; }

        [ForeignKey("PatientReferralDocumentId")]
        public PatientReferralDocument PatientReferralDocumentFk { get; set; }

        public virtual long? AttendingPhysicianId { get; set; }

        [ForeignKey("AttendingPhysicianId")]
        public StaffMember AttendingPhysicianFk { get; set; }

        public virtual long? ReferringClinicId { get; set; }

        [ForeignKey("ReferringClinicId")]
        public OrganizationUnitExtended ReferringClinicFk { get; set; }

        public virtual long? AttendingClinicId { get; set; }

        [ForeignKey("AttendingClinicId")]
        public OrganizationUnitExtended AttendingClinicFk { get; set; }

        public virtual string TransferredClinic { get; set; }
    }
}