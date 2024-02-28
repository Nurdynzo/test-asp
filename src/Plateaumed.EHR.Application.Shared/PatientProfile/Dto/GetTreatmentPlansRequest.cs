using System.ComponentModel.DataAnnotations;
using Plateaumed.EHR.Invoices.Dtos;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class GetTreatmentPlansRequest
    {
        [Required]
        public long PatientId { get; set; }
        public TreatmentPlanTimeFilter? Filter { get; set; }
    }
}
