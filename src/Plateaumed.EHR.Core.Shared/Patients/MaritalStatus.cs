using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
using System.Runtime.Serialization;

namespace Plateaumed.EHR.Patients
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MaritalStatus
    {
        [PgName("Single"), EnumMember(Value = "Single")]
        Single,

        [PgName("Married"), EnumMember(Value = "Married")]
        Married,

        [PgName("Divorced"), EnumMember(Value = "Divorced")]
        Divorced,

        [PgName("Widowed"), EnumMember(Value = "Widowed")]
        Widowed,

        [PgName("Separated"), EnumMember(Value = "Separated")]
        Separated
    }
}
