using System;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.PatientAppointments.Dtos;

public class AppointmentListResponse: EntityDto<long>
{
    public string Title { get; set; }

    public int Duration { get; set; }

    public DateTime StartTime { get; set; }

    public bool IsRepeat { get; set; }

    public string Notes { get; set; }

    public AppointmentRepeatType RepeatType { get; set; }

    public AppointmentStatusType Status { get; set; }

    public ScanningStatusType ScanningStatus { get; set; } = ScanningStatusType.NoScannedRecord;

    public AppointmentType Type { get; set; }

    public AppointmentPatientDto AppointmentPatient { get; set; }

    public ReferralDocumentDto ReferralDocument { get; set; }

    public AttendingPhysicianDto AttendingPhysician { get; set; }

    public string ReferringClinic { get; set; }

    public AttendingClinicDto AttendingClinic { get; set; }
    
    public long AppointmentCount { get; set; }

    public ScannedDocumentDto ScannedDocument { get; set; }
    
}