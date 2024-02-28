using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Diagnoses
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DiagnosisType
    {
        [PgName("Clinical"), EnumMember(Value = "Clinical")]
        Clinical,
        [PgName("Differential"), EnumMember(Value = "Differential")]
        Differential
    }
}
