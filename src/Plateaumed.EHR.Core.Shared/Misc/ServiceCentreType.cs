using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Misc
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ServiceCentreType
    {
        [PgName("OutPatient"), EnumMember(Value = "OutPatient")]
        OutPatient,
        [PgName("InPatient"), EnumMember(Value = "InPatient")]
        InPatient,
        [PgName("AccidentAndEmergency"), EnumMember(Value = "AccidentAndEmergency")]
        AccidentAndEmergency,
        [PgName("Pharmacy"), EnumMember(Value = "Pharmacy")]
        Pharmacy,
        [PgName("Laboratory"), EnumMember(Value = "Laboratory")]
        Laboratory,
        [PgName("Others"), EnumMember(Value = "Others")]
        Others
    }
}