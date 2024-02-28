using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;
using System.Runtime.Serialization;

namespace Plateaumed.EHR.Patients
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Relationship
    {
        [PgName("Husband"), EnumMember(Value = "Husband")]
        Husband,

        [PgName("Wife"), EnumMember(Value = "Wife")]
        Wife,

        [PgName("Father"), EnumMember(Value = "Father")]
        Father,

        [PgName("Mother"), EnumMember(Value = "Mother")]
        Mother,

        [PgName("Step-Father"), EnumMember(Value = "Step-Father")]
        Step_Father,

        [PgName("Step-Mother"), EnumMember(Value = "Step-Mother")]
        Step_Mother,

        [PgName("Son"), EnumMember(Value = "Son")]
        Son,

        [PgName("Daughter"), EnumMember(Value = "Daughter")]
        Daughter,

        [PgName("Step-Son"), EnumMember(Value = "Step-Son")]
        Step_Son,

        [PgName("Step-Daughter"), EnumMember(Value = "Step-Daughter")]
        Step_Daughter,

        [PgName("Brother"), EnumMember(Value = "Brother")]
        Brother,

        [PgName("Sister"), EnumMember(Value = "Sister")]
        Sister,

        [PgName("GrandParent"), EnumMember(Value = "GrandParent")]
        GrandParent,

        [PgName("GrandFather"), EnumMember(Value = "GrandFather")]
        GrandFather,

        [PgName("GrandMother"), EnumMember(Value = "GrandMother")]
        GrandMother,

        [PgName("GrandSon"), EnumMember(Value = "GrandSon")]
        GrandSon,

        [PgName("GrandDaughter"), EnumMember(Value = "GrandDaughter")]
        GrandDaughter,

        [PgName("Uncle"), EnumMember(Value = "Uncle")]
        Uncle,

        [PgName("Aunt"), EnumMember(Value = "A+")]
        Aunt,

        [PgName("Cousin"), EnumMember(Value = "Cousin")]
        Cousin,

        [PgName("Nephew"), EnumMember(Value = "Nephew")]
        Nephew,

        [PgName("Niece"), EnumMember(Value = "Niece")]
        Niece,

        [PgName("Father-In-Law"), EnumMember(Value = "Father-In-Law")]
        Father_In_Law,

        [PgName("Mother-In-Law"), EnumMember(Value = "Mother-In-Law")]
        Mother_In_Law,

        [PgName("Brother-In-Law"), EnumMember(Value = "Brother-In-Law")]
        Brother_In_Law,

        [PgName("Sister-In-Law"), EnumMember(Value = "Sister-In-Law")]
        Sister_In_Law,

        [PgName("Son-In-Law"), EnumMember(Value = "Son-In-Law")]
        Son_In_Law,

        [PgName("Daughter-In-Law"), EnumMember(Value = "Daughter-In-Law")]
        Daughter_In_Law,

        [PgName("Friend"), EnumMember(Value = "Friend")]
        Friend,

        [PgName("BoyFriend"), EnumMember(Value = "BoyFriend")]
        BoyFriend,

        [PgName("GirlFriend"), EnumMember(Value = "GirlFriend")]
        GirlFriend,
    }
}
