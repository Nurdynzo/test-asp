using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Dtos
{
    public class ActivateOrDeactivatePharmacyRequest : EntityDto<long?>
    {
        public bool HasPharmacy { get; set; }
    }
}
