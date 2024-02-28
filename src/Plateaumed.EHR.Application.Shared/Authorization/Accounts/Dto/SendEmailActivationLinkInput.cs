using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}