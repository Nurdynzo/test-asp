using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
namespace Plateaumed.EHR.PatientProfile
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MenstrualFlowType
    {
        [PgName("Regular"), EnumMember(Value = "Regular")]
        Regular,
        [PgName("Heavy"), EnumMember(Value = "Heavy")]
        Heavy,
    }
}