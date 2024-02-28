using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class ActivateBankRequest : EntityDto<long?>
    {
        public bool IsActive { get; set; }
    }
}
