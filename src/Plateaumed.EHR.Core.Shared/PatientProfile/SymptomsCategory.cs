using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
using System.Runtime.Serialization;

namespace Plateaumed.EHR.PatientProfile
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SymptomsCategory
    {
        [PgName("Systemic"), EnumMember(Value = "Systemic")]
        Systemic,
        [PgName("Cardiovascular"), EnumMember(Value = "Cardiovascular")]
        Cardiovascular,
        [PgName("Respiratory"), EnumMember(Value = "Respiratory")]
        Respiratory,
        [PgName("Gastrointestinal"), EnumMember(Value = "Gastrointestinal")]
        Gastrointestinal,
        [PgName("Genitourinary"), EnumMember(Value = "Genitourinary")]
        Genitourinary,
        [PgName("Musculoskeletal"), EnumMember(Value = "Musculoskeletal")]
        Musculoskeletal,
        [PgName("Dermatological"), EnumMember(Value = "Dermatological")]
        Dermatological,
        [PgName("Neurological"), EnumMember(Value = "Neurological")]
        Neurological
    }
}
