using Plateaumed.EHR.Diagnoses.Dto;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.VitalSigns.Dto;
using System.Collections.Generic;

namespace Plateaumed.EHR.ReviewAndSaves.Dtos
{
    public class EncounterSummaryDto
    {
        public long DoctorUserId { get; set; }
        public string Title { get; set; }  // Seen by Dr {full name}
        public List<string> Summary { get; set; }
        public List<PatientPhysicalExaminationDto> GeneralPhysicalExaminationResults { get; set; }
        public List<PatientVitalsSummaryResponseDto> VitalSignResults { get; set; }
        public List<InvestigationResultDto> InvestigationResults { get; set; }
        public List<DiagnosisDto> DiagnosisResults { get; set; }
        public DischargeDto DischargeResult { get; set; }
    }
}
