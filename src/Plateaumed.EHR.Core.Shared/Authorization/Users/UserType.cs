using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Authorization.Users
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserType
    {
        [PgName("Tenant_Staff"), EnumMember(Value = "Tenant_Staff")]
        TenantStaff,

        [PgName("Tenant_Patient"), EnumMember(Value = "Tenant_Patient")]
        TenantPatient,

        [PgName("Host"), EnumMember(Value = "Host")]
        Host
    }
}
