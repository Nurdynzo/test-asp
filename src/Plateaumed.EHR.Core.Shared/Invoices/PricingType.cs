using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
namespace Plateaumed.EHR.Invoices
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PricingType
    {
        [PgName("General_Pricing"), EnumMember(Value = "General Pricing")]
        GeneralPricing,
    }
}