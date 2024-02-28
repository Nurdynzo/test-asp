using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
namespace Plateaumed.EHR.Procedures;

[JsonConverter(typeof(StringEnumConverter))]
public enum NoteType
{
    [PgName("ProcedureNote"), EnumMember(Value = "ProcedureNote")]
    ProcedureNote,
    [PgName("AnaesthesiaNote"), EnumMember(Value = "AnaesthesiaNote")]
    AnaesthesiaNote,
    [PgName("NurseNote"), EnumMember(Value = "NurseNote")]
    NurseNote,
}
