using System.Collections.Generic;

namespace Plateaumed.EHR.Utility
{
    public static class MedicationSuggestionList
    {
        public static string[] Dose = new string[]
        {
            "apply",
            "apply sparingly",
            "0.1",
            "0.25",
            "0.5",
            "1",
            "2",
            "3",
            "4",
            "1-2",
            "2-3",
            "2-4",
            "3-4",
            "5",
            "5-10",
            "6",
            "7",
            "8",
            "9"
        };

        public static string[] Unit = new string[]
        {
            "mg",
            "ml",
            "g",
            "unit(s)",
            "IU",
            "Ug",
            "each",
            "inch(es)",
            "milliequivalent(s)",
            "drop(s)",
            "applicatorful",
            "scoop(s)",
            "day(s)",
            "hour(s)",
            "minute(s)",
            "suppository(ies)",
            "gum",
            "patch",
            "lozenge",
            "tab"
        };

        public static string[] Frequency = new string[]
        {
            "X times per hour",
            "X times per day",
            "X times per week",
            "every X minutes",
            "every X hour(s)",
            "every X day(s)",
            "every X week(s)",
            "every X month(s)",
            "Stat",
            "PRN"
        };

        public static string[] Direction = new string[]
        {
            "intravenous",
            "intramuscular",
            "apply generously",
            "apply sparingly",
            "as needed",
            "before meals",
            "after meals",
            "with meals", 
            "in the morning",
            "in the afternoon",
            "in the evening",
            "at bedtime",
            "alternate nostrils",
            "swish/spit",
            "swish/swallow"
        };

        public static string[] Duration = new string[]
        {
            "X doses",
            "X days",
            "X weeks",
            "X month"
        };
    }
}