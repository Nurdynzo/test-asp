using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NpgsqlTypes;

namespace Plateaumed.EHR.Patients
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ScanningStatusType
    {

        [PgName("No_scanned_record"), EnumMember(Value = "No scanned record")]
        NoScannedRecord,


        [PgName("Scanning_in_progres"), EnumMember(Value = "Scanning in progress")]
        ScanningInProgress,


        [PgName("Scanned_record_avaialbe"), EnumMember(Value = "Scanned record available")]
        ScannedRecordAvailable,
    }
}
