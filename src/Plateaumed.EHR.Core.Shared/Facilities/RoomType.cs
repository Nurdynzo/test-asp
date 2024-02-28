using Newtonsoft.Json.Converters;
using NpgsqlTypes;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Plateaumed.EHR.Facilities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RoomType
    {
        [PgName("OperatingRoom"), EnumMember(Value = "OperatingRoom")]
        OperatingRoom,

        [PgName("ConsultingRoom"), EnumMember(Value = "ConsultingRoom")]

        ConsultingRoom
    }
}
