using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Misc
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InvestigationStatus
	{
        [PgName("AllInvestigations"), EnumMember(Value = "All Investigations")]
        AllInvestigations,

        [PgName("Requested"), EnumMember(Value = "Requested")]
        Requested,

        [PgName("Invoiced"), EnumMember(Value = "Invoiced")]
        Invoiced,

        [PgName("Processing"), EnumMember(Value = "Processing")]
        Processing,

        [PgName("ResultReady"), EnumMember(Value = "Result Ready")]
        ResultReady,

        [PgName("ImageReady"), EnumMember(Value = "Image Ready")]
        ImageReady,

        [PgName("AwaitingReview"), EnumMember(Value = "Awaiting Review")]
        AwaitingReview,

        [PgName("ReportReady"), EnumMember(Value = "Report Ready")]
        ReportReady,
    }
}

