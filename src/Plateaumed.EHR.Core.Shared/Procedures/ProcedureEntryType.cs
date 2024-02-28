using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace Plateaumed.EHR.Procedures;

[JsonConverter(typeof(StringEnumConverter))]
public enum ProcedureEntryType
{
    Preop, 
    Intraop,
    Postop
}
