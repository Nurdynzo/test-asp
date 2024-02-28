using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
namespace Plateaumed.EHR.Patients
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ViewAllPatientInClinicSortFilter
    {
        [PgName("Patient"), EnumMember(Value = "Patient")]
        Patient,
        [PgName("AppointmentStatus"), EnumMember(Value = "AppointmentStatus")]
        AppointmentStatus,
        [PgName("PaymentStatus"), EnumMember(Value = "PaymentStatus")]
        PaymentStatus

    }
}