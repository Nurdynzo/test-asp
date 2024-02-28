using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Plateaumed.EHR.MultiTenancy
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TenantDocumentType
    {
        [PgName("Medical_Degree"), EnumMember(Value = "Medical_Degree")]
        MedicalDegree,

        [PgName("Practicing_License"), EnumMember(Value = "Practicing_License")]
        PracticingLicense
    }
}
