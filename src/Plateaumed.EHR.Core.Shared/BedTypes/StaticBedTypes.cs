using System.Collections.Generic;

namespace Plateaumed.EHR.BedTypes
{
    public static class StaticBedTypes
    {
        public const string Bed = "Bed";
        public const string Cradle = "Cradle";
        public const string Cot = "Cot";
        public const string Incubator = "Incubator";

        public static List<string> GetAll()
        {
            return new List<string>
            {
                Bed,
                Cradle,
                Cot,
                Incubator
            };
        }
    }
}