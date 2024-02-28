using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.MultiTenancy.Accounting.Dto;

namespace Plateaumed.EHR.MultiTenancy.Accounting
{
    public interface ITenantInvoiceAppService
    {
        Task<TenantInvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(TenantCreateInvoiceDto input);
    }
}
