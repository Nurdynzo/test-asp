using System;

namespace Plateaumed.EHR.PhysicalExaminations.Dto
{
    public class UploadedImageDto
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }
}
