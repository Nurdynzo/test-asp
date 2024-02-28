using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.DoctorDischarge
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DischargeEntryType
    {
        [PgName("NORMAL"), EnumMember(Value = "NORMAL")]
        NORMAL = 1,
        [PgName("DAMA"), EnumMember(Value = "DAMA")]
        DAMA = 2,
        [PgName("DECEASED"), EnumMember(Value = "DECEASED")]
        DECEASED = 3,
        [PgName("SUSPENSEADMISSION"), EnumMember(Value = "SUSPENSEADMISSION")]
        SUSPENSEADMISSION = 4
    }
}
