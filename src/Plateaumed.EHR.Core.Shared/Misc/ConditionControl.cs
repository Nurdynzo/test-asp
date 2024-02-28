using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Misc
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ConditionControl
    {
        [PgName("Well_Controlled"), EnumMember(Value = "Well Controlled")]
        WellControlled,
        [PgName("Poorly_Controlled"), EnumMember(Value = "Poorly Controlled")]
        PoorlyControlled
    }
}