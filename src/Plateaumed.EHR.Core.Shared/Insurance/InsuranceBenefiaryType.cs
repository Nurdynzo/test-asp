using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Plateaumed.EHR.Insurance
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InsuranceBenefiaryType
    {
        [PgName("Primary"), EnumMember(Value = "Primary")]
        Primary,

        [PgName("Dependent"), EnumMember(Value = "Dependent")]
        Dependent
    }
}
