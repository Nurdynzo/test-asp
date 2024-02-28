using System.Runtime.Serialization;
using NpgsqlTypes;

namespace Plateaumed.EHR.PhysicalExaminations;

public enum PlaneType
{
    [PgName("Left"), EnumMember(Value = "Left")]
    Left, 
    
    [PgName("Right"), EnumMember(Value = "Right")]
    Right,
    
    [PgName("Bilateral"), EnumMember(Value = "Bilateral")]
    Bilateral
}