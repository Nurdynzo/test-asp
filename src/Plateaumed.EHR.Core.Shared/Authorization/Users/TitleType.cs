using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Authorization.Users
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TitleType
    {
        [PgName("Mr"), EnumMember(Value = "Mr")]
        Mr,

        [PgName("Mrs"), EnumMember(Value = "Mrs")]
        Mrs,

        [PgName("Miss"), EnumMember(Value = "Miss")]
        Miss,

        [PgName("Ms"), EnumMember(Value = "Ms")]
        Ms,

        [PgName("Dr"), EnumMember(Value = "Dr")]
        Dr,

        [PgName("Prof"), EnumMember(Value = "Prof")]
        Prof,

        [PgName("Hon"), EnumMember(Value = "Hon")]
        Hon,

        [PgName("Rev"), EnumMember(Value = "Rev")]
        Rev,

        [PgName("Pr"), EnumMember(Value = "Pr")]
        Pr,

        [PgName("Fr"), EnumMember(Value = "Fr")]
        Fr,

        [PgName("Other"), EnumMember(Value = "Other")]
        Other,

        [PgName("CM"), EnumMember(Value = "CM")]
        Cm,

        [PgName("DNS"), EnumMember(Value = "DNS")]
        Dns,

        [PgName("ADNS"), EnumMember(Value = "ADNS")]
        Adns,

        [PgName("CNO"), EnumMember(Value = "CNO")]
        Cno,

        [PgName("ACNO"), EnumMember(Value = "ACNO")]
        Acno,

        [PgName("PNO"), EnumMember(Value = "PNO")]
        Pno,

        [PgName("SNO"), EnumMember(Value = "SNO")]
        Sno,

        [PgName("NO"), EnumMember(Value = "NO")]
        No,

        [PgName("Pharm"), EnumMember(Value = "Pharm")]
        Pharm,

        [PgName("Pharm Tech"), EnumMember(Value = "Pharm Tech")]
        PharmTech
    }
}