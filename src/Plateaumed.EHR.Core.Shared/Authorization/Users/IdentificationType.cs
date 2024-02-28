using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Authorization.Users
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IdentificationType
    {
        
        [PgName("State_Id_Card"), EnumMember(Value = "State_Id_Card")]
        StateIdCard,

        [PgName("State_Driver_License"), EnumMember(Value = "State_Driver_License")]
        StateDriverLicense,

        [PgName("Military_Id_Card"), EnumMember(Value = "Military_Id_Card")]
        MilitaryIdCard,

        [PgName("Social_Security_Card"), EnumMember(Value = "Social_Security_Card")]
        SocialSecurityCard,

        [PgName("Birth_Certificate"), EnumMember(Value = "Birth_Certificate")]
        BirthCertificate,

        [PgName("Voter_Registration_Card"), EnumMember(Value = "Voter_Registration_Card")]
        VoterRegistrationCard
    }
}
