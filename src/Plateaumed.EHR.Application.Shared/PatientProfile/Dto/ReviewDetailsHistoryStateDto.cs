using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class ReviewDetailsHistoryStateDto
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
        public long? Id { get; set; }
        public string LastEditorName { get; set; }
    }
}
