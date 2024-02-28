using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}