using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Patients;

[JsonConverter(typeof(StringEnumConverter))]
public enum EncounterStatusType
{
    [PgName("In_Progress"), EnumMember(Value = "In progress")]
    InProgress,
    
    [PgName("Transfer_In_Pending"), EnumMember(Value = "Transfer in pending")]
    TransferInPending,

    [PgName("Transfer_Out_Pending"), EnumMember(Value = "Transfer out pending")]
    TransferOutPending,

    [PgName("Transferred"), EnumMember(Value = "Transferred")]
    Transferred,

    [PgName("Discharge_Pending"), EnumMember(Value = "Discharge pending")]
    DischargePending,

    [PgName("Discharged"), EnumMember(Value = "Discharged")]
    Discharged,
    
    [PgName("Admission_Pending"), EnumMember(Value = "Admission pending")]
    AdmissionPending,

    [PgName("Completed"), EnumMember(Value = "Completed")]
    Completed,

    [PgName("Deceased"), EnumMember(Value = "Deceased")]
    Deceased
}