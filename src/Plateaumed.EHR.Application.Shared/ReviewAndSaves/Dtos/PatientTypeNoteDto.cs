using Plateaumed.EHR.PhysicalExaminations.Dto;
using System.Collections.Generic;

namespace Plateaumed.EHR.ReviewAndSaves.Dtos
{
    public class PatientTypeNoteDto
    {
        public string Type { get; set; }
        public string Note { get; set; }
        public List<UploadedImageDto> ImageFiles { get; set; }
        public bool ImageUploaded { get; set; }
    }
}
