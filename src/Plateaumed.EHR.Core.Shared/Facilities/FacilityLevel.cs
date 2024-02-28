using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
using System.Runtime.Serialization;

namespace Plateaumed.EHR.Facilities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FacilityLevel
    {
        [PgName("Primary"), EnumMember(Value = "Primary")]
        Primary,

        [PgName("Secondary"), EnumMember(Value = "Secondary")]
        Secondary,

        [PgName("Tertiary"), EnumMember(Value = "Tertiary")]
        Tertiary
    }
}
