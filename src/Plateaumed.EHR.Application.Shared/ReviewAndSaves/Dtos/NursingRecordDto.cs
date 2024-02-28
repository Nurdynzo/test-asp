using Plateaumed.EHR.Admissions.Dto;
using Plateaumed.EHR.BedMaking.Dtos;
using Plateaumed.EHR.Diagnoses.Dto;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.IntakeOutputs.Dtos;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Meals.Dtos;
using Plateaumed.EHR.Medication.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.NextAppointments.Dtos;
using Plateaumed.EHR.NurseCarePlans.Dto;
using Plateaumed.EHR.NurseHistory.Dtos;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.PlanItems.Dtos;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.ReferAndConsults.Dtos;
using Plateaumed.EHR.Vaccines.Dto;
using Plateaumed.EHR.VitalSigns.Dto;
using Plateaumed.EHR.WoundDressing.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.ReviewAndSaves.Dtos
{
    public class NursingRecordDto
    {
        public long? Id { get; set; }
        [Required]
        public long EncounterId { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        [Required]
        public NursingNoteDto NursingNote { get; set; }
        public bool? IsAutoSaved { get; set; }
    }
    public class NursingNoteDto
    {
        public List<PatientVitalsSummaryResponseDto> VitalSignResults { get; set; }
        public List<InvestigationResultDto> InvestigationResults { get; set; }
        public List<PatientProcedureResponseDto> Procedures { get; set; }
        public List<PatientPhysicalExaminationDto> PhysicalExaminationResults { get; set; }
        public List<PatientMedicationForReturnDto> Prescriptions { get; set; }
        //public int MiscellaneousInterventions { get; set; }
        public List<GetNurseCareSummaryResponse> NurseCare { get; set; }
        public List<NurseHistoryResponseDto> NursingHistory { get; set; }
        public TransferResultDto TransferResult { get; set; }
        public DischargeDto DischargeResult { get; set; }
        public List<WoundDressingSummaryForReturnDto> WoundDressing { get; set; }
        public List<MealsSummaryForReturnDto> Meals { get; set; }
        public List<PatientBedMakingSummaryForReturnDto> BedMaking { get; set; }
        public List<PatientIntakeOutputDto> IntakeOutput { get; set; }
    }
    public class TransferResultDto
    {
        public long PatientId { get; set; }
        public EncounterStatusType Status { get; set; }
        public DateTime? TimeOut { get; set; }
        public ServiceCentreType ServiceCentre { get; set; }
        public string UnitName { get; set; }
        public string FacilityName { get; set; }
        public string Ward { get; set; }
        public string WardBed { get; set; }
    }
}
