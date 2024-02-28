using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class ActivateDefaultBankRequest : EntityDto<long?>
    {
        public bool IsDefault { get; set; }
    }
}
