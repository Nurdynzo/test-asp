using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.IntakeOutputs
{
    public enum ChartingType
    {
        [PgName("INTAKE"), EnumMember(Value = "INTAKE")]
        INTAKE = 1,
        [PgName("OUTPUT"), EnumMember(Value = "OUTPUT")]
        OUTPUT = 2,
        [PgName("UNKNOWN"), EnumMember(Value = "UNKNOWN")]
        UNKNOWN = 3,
    }
}
