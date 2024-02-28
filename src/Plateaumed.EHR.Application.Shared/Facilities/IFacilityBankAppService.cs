using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Facilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities
{
    public interface IFacilityBanksAppService : IApplicationService
    {
        Task CreateOrEdit(CreateOrEditBankRequest request);

        Task CreateOrEditMultipleBanks(List<CreateOrEditBankRequest> request);

        Task ActivateBank(ActivateBankRequest request);

        Task MarkBankAsDefault(ActivateDefaultBankRequest request);

        Task<FacilityBankResponseDto> GetFacilityBanksForEdit(EntityDto<long> request);
    }
}