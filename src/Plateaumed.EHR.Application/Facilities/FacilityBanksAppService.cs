using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.UI;
using Plateaumed.EHR.Authorization;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.Staff.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities
{
    [AbpAuthorize(AppPermissions.Pages_FacilityBanks)]
    public class FacilityBanksAppService : EHRAppServiceBase, IFacilityBanksAppService
    {
        private readonly ICreateFacilityBankCommandHandler _createFacilityBank;
        private readonly IUpdateFacilityBankCommandHandler _updateFacilityBank;
        private readonly IActivateFacilityBankCommandHandler _activateFacilityBank;
        private readonly IActivateFacilityDefaultBankCommandHandler _activateDefaultBank;
        private readonly IGetFacilityBankQueryHandler _getFacilityBanks;
        private readonly IGetAllFacilityBankQueryHandler _getAllFacilityBankQuery;

        public FacilityBanksAppService(
            ICreateFacilityBankCommandHandler createCommandHandler,
            IUpdateFacilityBankCommandHandler updateFacilityBank,
            IActivateFacilityBankCommandHandler activateFacilityBank,
            IActivateFacilityDefaultBankCommandHandler activateDefaultBank,
            IGetFacilityBankQueryHandler getFacilityBanks,
            IGetAllFacilityBankQueryHandler getAllFacilityBankQuery)
        {
            _createFacilityBank = createCommandHandler;
            _updateFacilityBank = updateFacilityBank;
            _activateFacilityBank = activateFacilityBank;
            _activateDefaultBank = activateDefaultBank;
            _getFacilityBanks = getFacilityBanks;
            _getAllFacilityBankQuery = getAllFacilityBankQuery;
        }

        public async Task CreateOrEdit(CreateOrEditBankRequest input)
        {
            if (!input.Id.HasValue)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityBanks_Edit)]
        public async Task CreateOrEditMultipleBanks(List<CreateOrEditBankRequest> request)
        {
            foreach (CreateOrEditBankRequest bank in request)
            {
                await CreateOrEdit(bank);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityBanks_Edit)]
        public virtual async Task ActivateBank(ActivateBankRequest request)
        {
            await _activateFacilityBank.Handle(request);
        }

        [AbpAuthorize(AppPermissions.Pages_Facilities_Edit)]
        public virtual async Task MarkBankAsDefault(ActivateDefaultBankRequest request)
        {
            await _activateDefaultBank.Handle(request);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityBanks_Edit)]
        public async Task<FacilityBankResponseDto> GetFacilityBanksForEdit(EntityDto<long> request)
        {
            return await _getFacilityBanks.Handle(request);
        }

        public async Task<PagedResultDto<GetFacilityBankForViewDto>> GetAllFacilityBank(GetAllFacilityBanksInput request)
        {
            return await _getAllFacilityBankQuery.Handle(request);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityBanks_Create)]
        protected virtual async Task Create(CreateOrEditBankRequest request)
        {
            await _createFacilityBank.Handle(request);
        }

        [AbpAuthorize(AppPermissions.Pages_FacilityBanks_Edit)]
        protected virtual async Task Update(CreateOrEditBankRequest request)
        {
            await _updateFacilityBank.Handle(request);
        }
    }
}