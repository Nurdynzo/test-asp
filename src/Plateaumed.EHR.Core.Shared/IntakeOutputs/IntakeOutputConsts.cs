using System;
using System.Text;

namespace Plateaumed.EHR.IntakeOutputs
{
    /// <summary>
    /// Some consts used in the application.
    /// </summary>
    public class IntakeOutputConsts
    {
        /// <summary>
        ///  Infusion Constants - Intake
        /// </summary>
        public const string WATER = "Water";
        public const string PAP = "Pap";
        public const string CARBONATED_DRINKS = "Carbonated drinks";
        public const string ORAL_FLUIDS = "Oral fluids";

        /// <summary>
        /// IV Medication - Intake
        /// </summary>
        public const string LINE_RESET = "For IV line reset";
        public static string FLUIDS_INTERRUPTED = "IV fluids interrupted";
        public static string FLUIDS_NOT_AVAILABLE = "IV fluids not available";
        public static string TREATMENT_SENT = "Treatment sheet sent to Pharmacy";
        public static string FLUIDS_NOT_SUPPLIED = "IV fluids not supplied from Pharmacy";

        /// <summary>
        /// Output
        /// </summary>
        public const string URINE = "Urine";
        public const string WET_DIAPER = "Wet diaper";
        public const string WET_BED = "Wet bed linen";
        public const string STOMA_BAG = "Stoma bag fluid";
    }
}






