using System;

namespace Plateaumed.EHR.ReviewAndSaves.Dtos
{
    public class PresentingComplaintDto
    {
        public long Id { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
