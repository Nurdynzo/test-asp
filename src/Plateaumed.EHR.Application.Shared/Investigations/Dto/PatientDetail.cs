using System;
namespace Plateaumed.EHR.Investigations.Dto
{
    public class PatientDetail
    {
        public long PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PatientDisplayName { get; set; }
        public string PatientImageUrl { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string PatientCode { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

