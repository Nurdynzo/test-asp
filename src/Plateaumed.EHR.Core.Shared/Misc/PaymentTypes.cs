using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
using System.Runtime.Serialization;

namespace Plateaumed.EHR.Misc
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentTypes
    {
        [PgName("ServiceOnCredit"), EnumMember(Value = "ServiceOnCredit")]
        ServiceOnCredit,

        [PgName("Wallet"), EnumMember(Value = "Wallet")]
        Wallet,
        
        [PgName("SplitPayment"), EnumMember(Value = "SplitPayment")]
        SplitPayment,

        [PgName("Insurance"), EnumMember(Value = "Insurance")]
        Insurance,
    }
}
