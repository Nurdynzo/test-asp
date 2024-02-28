using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Authorization.Users
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GenderType
    {
        [PgName("Male"), EnumMember(Value = "Male")]
        Male,

        [PgName("Female"), EnumMember(Value = "Female")]
        Female,

        [PgName("Other"), EnumMember(Value = "Other")]
        Other
    }
}
