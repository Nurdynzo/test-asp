using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Misc
{
    
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InvoiceSource
    {
        [PgName("AccidentAndEmergency"), EnumMember(Value = "AccidentAndEmergency")]
        AccidentAndEmergency,
        [PgName("OutPatient"), EnumMember(Value = "OutPatient")]
        OutPatient,
        [PgName("InPatient"), EnumMember(Value = "InPatient")]
        InPatient,
        [PgName("Pharmacy"), EnumMember(Value = "Pharmacy")]
        Pharmacy,
        [PgName("Lab"), EnumMember(Value = "Lab")]
        Lab,
        [PgName("Others"), EnumMember(Value = "Others")]
        Others
    }
    
}