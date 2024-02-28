using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Misc
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentStatus
    {
        [PgName("Unpaid"), EnumMember(Value = "Unpaid")]
        Unpaid,
        [PgName("Paid"), EnumMember(Value = "Paid")]
        Paid,
        [PgName("PartiallyPaid"), EnumMember(Value = "PartiallyPaid")]
        PartiallyPaid,
        [PgName("Proforma"), EnumMember(Value = "Proforma")]
        Proforma
        
    }
}
