using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
using System.Runtime.Serialization;

namespace Plateaumed.EHR.Patients
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BloodGroup
    {
        [PgName("A+"), EnumMember(Value = "A+")]
        A_Positive,

        [PgName("A-"), EnumMember(Value = "A-")]
        A_Negative,

        [PgName("B+"), EnumMember(Value = "B+")]
        B_Positive,

        [PgName("B-"), EnumMember(Value = "B-")]
        B_Negative,

        [PgName("O+"), EnumMember(Value = "O+")]
        O_Positive,

        [PgName("O-"), EnumMember(Value = "O-")]
        O_Negative,

        [PgName("AB+"), EnumMember(Value = "AB+")]
        AB_Positive,

        [PgName("AB-"), EnumMember(Value = "AB-")]
        AB_Negative,
    }
}
