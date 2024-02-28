using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Patients
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BloodGroupAndGenotypeSource
    {
        [PgName("ClinicalInvestigation"), EnumMember(Value = "ClinicalInvestigation")]
        ClinicalInvestigation,

        [PgName("SelfReport"), EnumMember(Value = "SelfReport")]
        SelfReport
    }
}