using System.Runtime.Serialization;
using NpgsqlTypes;

namespace Plateaumed.EHR.Organizations
{
    public enum OrganizationUnitType
    {
        [PgName("Unit"), EnumMember(Value = "Unit")]
        Unit,
        [PgName("Department"), EnumMember(Value = "Department")]
        Department,
        [PgName("Facility"), EnumMember(Value = "Facility")]
        Facility,
        [PgName("Clinic"), EnumMember(Value = "Clinic")]
        Clinic
    }
}
