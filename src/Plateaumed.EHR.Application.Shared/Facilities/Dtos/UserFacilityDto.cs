using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class UserFacilityDto :  EntityDto<long>
    {
        public virtual string Name { get; set; }

        public string TenancyName { get; set; }

        public Guid? LogoId { get; set; }

        public string City { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public bool IsActive { get; set; }
    }
}
