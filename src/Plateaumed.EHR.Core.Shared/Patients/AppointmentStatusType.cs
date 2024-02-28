using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Patients
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AppointmentStatusType
    {
        [PgName("Pending"), EnumMember(Value = "Pending")]
        Pending,

        [PgName("Executed"), EnumMember(Value = "Executed")]
        Executed,

        [PgName("Missed"), EnumMember(Value = "Missed")]
        Missed,

        [PgName("Rescheduled"), EnumMember(Value = "Rescheduled")]
        Rescheduled,

        [PgName("Not_Arrived"), EnumMember(Value = "Not arrived")]
        Not_Arrived,

        [PgName("Arrived"), EnumMember(Value = "Arrived")]
        Arrived,

        [PgName("Processing"), EnumMember(Value = "Processing")]
        Processing,

        [PgName("Awaiting_Vitals"), EnumMember(Value = "Awaiting vitals")]
        Awaiting_Vitals,

        [PgName("Awaiting_Clinician"), EnumMember(Value = "Awaiting clinician")]
        Awaiting_Clinician,

        [PgName("Awaiting_Doctor"), EnumMember(Value = "Awaiting doctor")]
        Awaiting_Doctor,

        [PgName("Seen_Doctor"), EnumMember(Value = "Seen doctor")]
        Seen_Doctor,

        [PgName("Seen_Clinician"), EnumMember(Value = "Seen clinician")]
        Seen_Clinician,

        [PgName("Admitted_To_Ward"), EnumMember(Value = "Admitted to ward")]
        Admitted_To_Ward,

        [PgName("Tranferred"), EnumMember(Value = "Tranferred")]
        Tranferred,

        [PgName("Awaiting_Admission"), EnumMember(Value = "Awaiting admission")]
        Awaiting_Admission,
    }
}
