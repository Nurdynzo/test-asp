using Plateaumed.EHR.Admissions.Dto;
using Plateaumed.EHR.Diagnoses.Dto;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Medication.Dtos;
using Plateaumed.EHR.NextAppointments.Dtos;
using Plateaumed.EHR.PlanItems.Dtos;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.ReferAndConsults.Dtos;
using Plateaumed.EHR.Vaccines.Dto;
using Plateaumed.EHR.VitalSigns.Dto;
using System.Collections.Generic;

namespace Plateaumed.EHR.ReviewAndSaves.Dtos
{
    public class DoctorNoteDto
    {
        public string Title { get; set; }  //[Patient seen] or [Patient not on bed] or [Patient alive and well] 
        public string Summary { get; set; }
        public List<PresentingComplaintDto> PresentingComplaintResults { get; set; }
        public List<PatientTypeNoteDto> TypeNoteResults { get; set; }
        public List<PatientPhysicalExaminationDto> GeneralPhysicalExaminationResults { get; set; }
        public List<PatientVitalsSummaryResponseDto> VitalSignResults { get; set; }
        public List<InvestigationResultDto> InvestigationResults { get; set; }
        public Plans PlanResults { get; set; }
        public List<DiagnosisDto> DiagnosisResults { get; set; }
        public DischargeDto DischargeResult { get; set; }
    }

    public class Plans
    {
        public List<GetInvestigationRequestsResponse> InvestigationRequest { get; set; }
        public List<PatientMedicationForReturnDto> Prescriptions { get; set; }
        public List<PatientProcedureResponseDto> Procedures { get; set; }
        public List<PlanItemsSummaryForReturnDto> PlanItems { get; set; }
        public List<VaccinationResponseDto> Vaccinations { get; set; }
        public ReferralOrConsultReturnDto Referrals { get; set; }
        public ReferralOrConsultReturnDto Consults { get; set; }
        public List<NextAppointmentReturnDto> Appointments { get; set; }
        public AdmitPatientRequest Admission { get; set; }
    }
}