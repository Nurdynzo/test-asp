using System.Runtime.Serialization;
using NpgsqlTypes;

namespace Plateaumed.EHR.Patients
{

    public enum Intensity
    {
        [PgName("Low"), EnumMember(Value = "Low")]
        Low,

        [PgName("Moderate"), EnumMember(Value = "Moderate")]
        Moderate,

        [PgName("High"), EnumMember(Value = "High")]
        High
    }

}