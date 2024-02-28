using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plateaumed.EHR.PatientProfile
{
    [Table("ReviewDetailsHistoryStates")]
    public class ReviewDetailsHistoryState : FullAuditedEntity<long>
    {
        public long PatientId { get; set; }
        public bool NoFamilyHistory { get; set; }
        public bool NoPhysicalExerciseHistory { get; set; }
        public bool NoChronicIllness { get; set; }
        public bool NoMajorInjuries { get; set; }
        public bool NoTravelHistory { get; set; }
        public bool NoSurgicalHistory { get; set; }
        public bool NoBloodTransfusionHistory { get; set; }
        public bool NoVaccinationHistory { get; set; }
        public bool NoUseOfContraceptives { get; set; }
        public bool NoGynaecologicalIllness { get; set; }
        public bool NoGynaecologicalSurgery { get; set; }
        public bool NoHistoryOfCervicalScreening { get; set; }
        public bool NeverBeenPregnant { get; set; }
        public bool NoDeliveryDetails { get; set; }
        public bool PatientDoesNotTakeAlcohol { get; set; }
        public bool PatientDoesNotSmoke { get; set; }
        public bool NoUseOfRecreationalDrugs { get; set; }
        public bool NotCurrentlyOnMedication { get; set; }
        public bool NoAllergies { get; set; }
        public bool NoImplant { get; set; }
        public string LastEditorName { get; set; }

        public ReviewDetailsHistoryState()
        {
            PatientDoesNotTakeAlcohol = false;
            PatientDoesNotSmoke = false;
            NoFamilyHistory = false;
            NoPhysicalExerciseHistory = false;
            NoBloodTransfusionHistory = false;
            NoChronicIllness = false;
            NoMajorInjuries = false;
            NoTravelHistory = false;
            NoSurgicalHistory = false;
            NoVaccinationHistory = false;
            NoUseOfContraceptives = false;
            NoGynaecologicalIllness = false;
            NoGynaecologicalSurgery = false;
            NoHistoryOfCervicalScreening = false;
            NeverBeenPregnant = false;
            NoDeliveryDetails = false;
            NoUseOfRecreationalDrugs = false;
            NotCurrentlyOnMedication = false;
            NoAllergies = false;
            NoImplant = false;
        }
    }
}
