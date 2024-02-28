using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Invoices
{
    public interface IInvoiceItemsAppService : IApplicationService
    {
        Task<PagedResultDto<GetInvoiceItemForViewDto>> GetAll(GetAllInvoiceItemsInput input);

        Task<GetInvoiceItemForEditOutput> GetInvoiceItemForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditInvoiceItemDto input);

        Task Delete(EntityDto<long> input);

    }
}