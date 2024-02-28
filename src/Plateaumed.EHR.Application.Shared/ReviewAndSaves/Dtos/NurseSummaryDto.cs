using Plateaumed.EHR.Diagnoses.Dto;
using Plateaumed.EHR.Discharges.Dtos;
using Plateaumed.EHR.VitalSigns.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.ReviewAndSaves.Dtos
{
    public class NurseSummaryDto
    {
        public long NurseUserId { get; set; }
        public string Title { get; set; }
        public string NursingDiagnosisText { get; set; }
        public string NursingEvaluationText { get; set; }
        public List<PatientPhysicalExaminationDto> GeneralPhysicalExaminationResults { get; set; }
        public List<PatientVitalsSummaryResponseDto> VitalSignResults { get; set; }
        public List<InvestigationResultDto> InvestigationResults { get; set; }
    }
}
