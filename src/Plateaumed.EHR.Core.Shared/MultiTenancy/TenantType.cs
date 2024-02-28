using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NpgsqlTypes;

namespace Plateaumed.EHR.MultiTenancy
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TenantType
    {
        [PgName("Individual"), EnumMember(Value = "Individual")]
        Individual,

        [PgName("Business"), EnumMember(Value = "Business")]
        Business
    }
}

