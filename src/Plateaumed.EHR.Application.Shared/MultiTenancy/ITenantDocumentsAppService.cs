using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.MultiTenancy.Dtos;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.MultiTenancy
{
    public interface ITenantDocumentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetTenantDocumentForViewDto>> GetAll(GetAllTenantDocumentsInput input);

        Task<GetTenantDocumentForEditOutput> GetTenantDocumentForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditTenantDocumentDto input);

        Task Delete(EntityDto<long> input);

        Task RemoveDocumentFile(EntityDto<long> input);
    }
}
