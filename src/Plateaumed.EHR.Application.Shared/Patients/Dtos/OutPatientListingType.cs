using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Patients.Dtos;

[JsonConverter(typeof(StringEnumConverter))]
public enum OutPatientListingType
{
    [PgName("AttendingPhysician"), EnumMember(Value = "AttendingPhysician")]
    AttendingPhysician,

    [PgName("AttendingClinic"), EnumMember(Value = "AttendingClinic")]
    AttendingClinic
}