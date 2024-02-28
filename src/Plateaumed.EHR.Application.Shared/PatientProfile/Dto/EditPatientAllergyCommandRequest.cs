using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
namespace Plateaumed.EHR.PatientProfile.Dto
{
    public class EditPatientAllergyCommandRequest: EntityDto<long>
    {
        public string AllergyType { get; set; }

        public long? AllergySnomedId { get; set; }

        public string Reaction { get; set; }

        public long? ReactionSnomedId { get; set; }

        public string Notes { get; set; }

        public Severity Severity { get; set; }

        public string Interval { get; set; }
    }
}
