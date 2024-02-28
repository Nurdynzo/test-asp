using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
using System.Runtime.Serialization;

namespace Plateaumed.EHR.Patients
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Religion
    {
        [PgName("Christianity"), EnumMember(Value = "Christianity")]
        Christianity,

        [PgName("Islam"), EnumMember(Value = "Islam")]
        Islam,

        [PgName("African Traditional Religion"), EnumMember(Value = "African Traditional Religion")]
        African_Traditional_Religion,

        [PgName("Agnosticism"), EnumMember(Value = "Agnosticism")]
        Agnosticism,

        [PgName("Atheism"), EnumMember(Value = "Atheism")]
        Atheism,

        [PgName("Babism"), EnumMember(Value = "Babism")]
        Babism,

        [PgName("Bahai Faith"), EnumMember(Value = "Bahai Faith")]
        Bahai_Faith,

        [PgName("Buddhism"), EnumMember(Value = "Buddhism")]
        Buddhism,

        [PgName("Caodaism"), EnumMember(Value = "Caodaism")]
        Caodaism,

        [PgName("Cheondogyo"), EnumMember(Value = "Cheondogyo")]
        Cheondogyo,

        [PgName("Confucianism"), EnumMember(Value = "Confucianism")]
        Confucianism,

        [PgName("Daejongism"), EnumMember(Value = "Daejongism")]
        Daejongism,

        [PgName("Druze"), EnumMember(Value = "Druze")]
        Druze,

        [PgName("Hinduism"), EnumMember(Value = "Hinduism")]
        Hinduism,

        [PgName("Jainism"), EnumMember(Value = "Jainism")]
        Jainism,

        [PgName("Judaism"), EnumMember(Value = "Judaism")]
        Judaism,

        [PgName("Mandaeism"), EnumMember(Value = "Mandaeism")]
        Mandaeism,

        [PgName("Rastafarianism"), EnumMember(Value = "Rastafarianism")]
        Rastafarianism,

        [PgName("Ryukuan Religion"), EnumMember(Value = "Ryukuan Religion")]
        Ryukuan_Religion,

        [PgName("Shamanism"), EnumMember(Value = "Shamanism")]
        Shamanism,

        [PgName("Shintoism"), EnumMember(Value = "Shintoism")]
        Shintoism,

        [PgName("Shugendo"), EnumMember(Value = "Shugendo")]
        Shugendo,

        [PgName("Sikhism"), EnumMember(Value = "Sikhism")]
        Sikhism,

        [PgName("Taoism"), EnumMember(Value = "Taoism")]
        Taoism,

        [PgName("Yarsanism"), EnumMember(Value = "Yarsanism")]
        Yarsanism,

        [PgName("Yazdanism"), EnumMember(Value = "Yazdanism")]
        Yazdanism,

        [PgName("Zoroastrianism"), EnumMember(Value = "Zoroastrianism")]
        Zoroastrianism,
    }
}
