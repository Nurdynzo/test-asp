using Newtonsoft.Json.Converters;
using NpgsqlTypes;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Plateaumed.EHR.ReferAndConsults
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InputType
    {
        [PgName("Referral"), EnumMember(Value = "Referral")]
        Referral,

        [PgName("Consult"), EnumMember(Value = "Consult")]
        Consult
    }
}
