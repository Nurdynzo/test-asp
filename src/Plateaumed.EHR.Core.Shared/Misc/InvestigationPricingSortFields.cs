using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
namespace Plateaumed.EHR.Misc
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InvestigationPricingSortFields
	{
        [PgName("Test_Name_Asc"), EnumMember(Value = "Test name: A-Z")]
        TestNameAsc,
        [PgName("Test_Name_Desc"), EnumMember(Value = "Test name: Z-A")]
        TestNameDesc,
        [PgName("Price_Highest"), EnumMember(Value = "Price: Highest")]
        PriceHighest,
        [PgName("Price_Lowest"), EnumMember(Value = "Price: Lowest")]
        PriceLowest,
        [PgName("Date_Most_Recent"), EnumMember(Value = "Date: Most Recent")]
        DateMostRecent
    }
}

