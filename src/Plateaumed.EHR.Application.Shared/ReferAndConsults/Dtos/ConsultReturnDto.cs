using Plateaumed.EHR.Diagnoses.Dto;
using Plateaumed.EHR.VitalSigns.Dto;
using System.Collections.Generic;

namespace Plateaumed.EHR.ReferAndConsults.Dtos;

public class ConsultReturnDto
{
    public string OriginatingUnit { get; set; }
    public string OriginatingConsultant { get; set; }
    public string PatientName { get; set; }
    public string PatientAge { get; set; }
    public string PatientGender { get; set; }
    public string HospitalNumber { get; set; }
    public string IssuingDoctorName { get; set; }
    public string PatientID { get; set; }

    public string OriginatingUnitName { get; set; }
    public string OriginatingConsultantName { get; set; }
    public List<string> SummaryNotes { get; set; }
    public string PhysicalExaminationNotes { get; set; }
    public List<PatientVitalsSummaryResponseDto> VitalSignNotes { get; set; }
    public List<DiagnosisDto> Diagnosis { get; set; }
    public string ReceivingUnit { get; set; }
    public string ReceivingConsultant { get; set; }
}

