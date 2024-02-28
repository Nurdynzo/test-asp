using Plateaumed.EHR.Patients;

using System;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class PatientAppointmentDto : EntityDto<long>
    {
        public string Title { get; set; }

        public int Duration { get; set; }

        public DateTime StartTime { get; set; }

        public bool IsRepeat { get; set; }

        public string Notes { get; set; }

        public AppointmentRepeatType RepeatType { get; set; }

        public AppointmentStatusType Status { get; set; }

        public AppointmentType Type { get; set; }

        public long PatientId { get; set; }

        public long? PatientReferralId { get; set; }

        public long? AttendingPhysician { get; set; }

        public long? ReferringClinic { get; set; }

        public long? AttendingClinic { get; set; }

    }
}