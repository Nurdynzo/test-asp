using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Staff.Dtos;
using Plateaumed.EHR.Dto;

namespace Plateaumed.EHR.Staff
{
    public interface IStaffCodeTemplatesAppService : IApplicationService
    {
        Task<GetStaffCodeTemplateForEditOutput> GetStaffCodeTemplateForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditStaffCodeTemplateDto input);

        Task Delete(EntityDto<long> input);
    }
}
