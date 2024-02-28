using System;
using System.ComponentModel.DataAnnotations;
namespace Plateaumed.EHR.Procedures.Dtos
{
    public class CreateSpecializedProcedureNurseDetailCommand
    {
        [Required]
        public TimeOnly TimePatientReceived { get; set; }
        [Required]
        public long ProcedureId { get; set; }
        public long? ScrubStaffMemberId { get; set; }
        public long? CirculatingStaffMemberId { get; set; }
    }
}
