using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Configuration.Host.Dto
{
    public class UserEmailSettingsEditDto
    {
        public bool EnableEmailExpiration { get; set; }

        [Range(AppConsts.MinEmailActivationExpirationHours, AppConsts.MaxEmailActivationExpirationHours)]
        public int EmailActivationExpirationHours { get; set; }
    }
}
