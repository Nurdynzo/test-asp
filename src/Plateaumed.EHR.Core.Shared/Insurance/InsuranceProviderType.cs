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
    public enum InsuranceProviderType
    {
        [PgName("National"), EnumMember(Value = "National")]
        National,

        [PgName("State"), EnumMember(Value = "State")]
        State,

        [PgName("Private"), EnumMember(Value = "Private")]
        Private
    }
}
