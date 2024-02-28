using Abp.AutoMapper;
using Plateaumed.EHR.MultiTenancy.Dto;

namespace Plateaumed.EHR.Mobile.MAUI.Models.Tenants
{
    [AutoMapFrom(typeof(TenantListDto))]
    [AutoMapTo(typeof(TenantEditDto), typeof(CreateTenantInput))]
    public class TenantListModel : TenantListDto
    {
 
    }
}
