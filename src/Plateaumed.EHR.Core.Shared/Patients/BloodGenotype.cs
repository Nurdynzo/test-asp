using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
using System.Runtime.Serialization;

namespace Plateaumed.EHR.Patients
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BloodGenotype
    {
        [PgName("AA"), EnumMember(Value = "AA")]
        AA,

        [PgName("AS"), EnumMember(Value = "AS")]
        AS,

        [PgName("AC"), EnumMember(Value = "AC")]
        AC,

        [PgName("SS"), EnumMember(Value = "SS")]
        SS,

        [PgName("SC"), EnumMember(Value = "SC")]
        SC,
    }
}
