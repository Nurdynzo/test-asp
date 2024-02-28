using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;

namespace Plateaumed.EHR.Web.Models.TokenAuth
{
    public class TokenVerificationModel
    {
        [Required]
        [MaxLength(AbpUserBase.MaxEmailAddressLength)]
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        public string Token { get; set; }

        public string TenancyName { get; set; }
    }
}