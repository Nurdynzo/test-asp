using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
