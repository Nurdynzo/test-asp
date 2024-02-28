using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Misc
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PatientStabilityStatus
    {
        [PgName("Stable"), EnumMember(Value = "Patient is Stable")]
        Stable,

        [PgName("CriticallyIll"), EnumMember(Value = "Patient is critically Ill")]
        CriticallyIll,
    }
}

