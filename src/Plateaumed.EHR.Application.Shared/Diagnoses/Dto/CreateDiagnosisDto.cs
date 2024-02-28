using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Authorization.Users;

namespace Plateaumed.EHR.Diagnoses.Dto
{
    public class CreateDiagnosisDto
    {
        public long PatientId { get; set; }
        public long Sctid { get; set; }

        [StringLength(UserConsts.MaxNoteLength, MinimumLength = UserConsts.MinNoteLength)]
        public string Notes { get; set; }
        public List<DiagnosisItemDto> SelectedDiagnoses { get; set; }
        public long EncounterId { get; set; }
    }
}
