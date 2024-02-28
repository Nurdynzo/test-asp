using System.Collections.Generic;

namespace Plateaumed.EHR.Procedures.Dtos
{
    public class SelectedProceduresDto
    {
        public long ProcedureId { get; set; }
        public List<SelectedProcedureListDto> SelectedProcedure { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedUser { get; set; }
        public ProcedureStatus? Status { get; set; }
    }    
}
