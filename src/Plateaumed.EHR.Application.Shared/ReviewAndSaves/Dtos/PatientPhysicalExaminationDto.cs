using Plateaumed.EHR.PhysicalExaminations.Dto;
using System;
using System.Collections.Generic;

namespace Plateaumed.EHR.ReviewAndSaves.Dtos
{
    public class PatientPhysicalExaminationDto
    {
        public string Header { get; set; }
        public string Answer { get; set; }
        public List<UploadedImageDto> ImageFiles { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool ImageUploaded { get; set; }
    }
}
