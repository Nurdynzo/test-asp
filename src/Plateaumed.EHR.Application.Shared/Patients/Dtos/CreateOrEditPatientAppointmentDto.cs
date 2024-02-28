using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class CreateOrEditPatientAppointmentDto : EntityDto<long?>
    {

        [StringLength(PatientAppointmentConsts.MaxTitleLength, MinimumLength = PatientAppointmentConsts.MinTitleLength)]
        public string Title { get; set; }

        public int Duration { get; set; }

        public DateTime StartTime { get; set; }

        public bool IsRepeat { get; set; }

        [StringLength(PatientAppointmentConsts.MaxNotesLength, MinimumLength = PatientAppointmentConsts.MinNotesLength)]
        public string Notes { get; set; }

        public AppointmentRepeatType RepeatType { get; set; }

        public AppointmentStatusType Status { get; set; }

        public AppointmentType Type { get; set; }

        public long PatientId { get; set; }

        public long? PatientReferralId { get; set; }

        public long? AttendingPhysicianId { get; set; }

        public long? ReferringClinicId { get; set; }

        public long? AttendingClinicId { get; set; }

        public string TransferredClinic { get; set; }
        public string DiagnosisSummary { get; set; }
    }
}