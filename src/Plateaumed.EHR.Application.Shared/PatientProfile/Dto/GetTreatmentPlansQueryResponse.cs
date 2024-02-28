using System;
using System.Collections.Generic;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetTreatmentPlansQueryResponse
    {
        public string Diagnosis { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public List<TreatmentMedication> TreatmentMedication { get; set; }
        public TreatmentVaccination TreatmentVaccination { get; set; }
        public List<TreatmentProcedure> TreatmentProcedures { get; set; }
        public List<TreatmentOtherPlanItems> TreatmentOtherPlanItems { get; set; }
    }

}
