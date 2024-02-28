using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.PatientProfile
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Severity
    {
        [PgName("Mild"), EnumMember(Value = "Mild")]
        Mild,
        [PgName("Moderate"), EnumMember(Value = "Moderate")]
        Moderate,
        [PgName("Severe"), EnumMember(Value = "Severe")]
        Severe
    }
}