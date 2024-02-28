using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Patients
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AppointmentType
    {
        [PgName("Walk_In"), EnumMember(Value = "Walk-in")]
        Walk_In,

        [PgName("Referral"), EnumMember(Value = "Referral")]
        Referral,

        [PgName("Consultation"), EnumMember(Value = "Consultation")]
        Consultation,

        [PgName("Follow_Up"), EnumMember(Value = "Follow-up")]
        Follow_Up,

        [PgName("Medical_Exam"), EnumMember(Value = "Medical-exam")]
        Medical_Exam
    }
}
