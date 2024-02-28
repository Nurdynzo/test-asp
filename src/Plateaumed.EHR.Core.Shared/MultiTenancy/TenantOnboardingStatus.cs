using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.MultiTenancy
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TenantOnboardingStatus
    {
        [PgName("Facility_Details"), EnumMember(Value = "Facility_Details")]
        FacilityDetails,

        [PgName("Documentation"), EnumMember(Value = "Documentation")]
        Documentation,

        [PgName("Additional_Details"), EnumMember(Value = "Additional_Details")]
        AdditionalDetails,

        [PgName("Departments"), EnumMember(Value = "Departments")]
        Departments,

        [PgName("Clinics"), EnumMember(Value = "Clinics")]
        Clinics,
         
        [PgName("Wards"), EnumMember(Value = "Wards")]
        Wards,

        [PgName("Job_Titles_And_Levels"), EnumMember(Value = "Job_Titles_And_Levels")]
        JobTitlesAndLevels,

        [PgName("Staff"), EnumMember(Value = "Staff")]
        Staff,

        [PgName("Review_Details"), EnumMember(Value = "Review_Details")]
        ReviewDetails,

        [PgName("Finalize"), EnumMember(Value = "Finalize")]
        Finalize,

        [PgName("Trial"), EnumMember(Value = "Trial")]
        Trial,

        [PgName("Checkout"), EnumMember(Value = "Checkout")]
        Checkout,
    }
}