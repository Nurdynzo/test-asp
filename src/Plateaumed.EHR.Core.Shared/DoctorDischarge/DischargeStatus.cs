using Newtonsoft.Json.Converters;
using NpgsqlTypes;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Plateaumed.EHR.DoctorDischarge
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DischargeStatus
    {
        [PgName("INITIATED"), EnumMember(Value = "INITIATED")]
        INITIATED = 1,
        [PgName("TRANSFERRED"), EnumMember(Value = "TRANSFERRED")]
        TRANSFERRED = 2,
        [PgName("FINALIZED"), EnumMember(Value = "FINALIZED")]
        FINALIZED  = 3,
    }
}
