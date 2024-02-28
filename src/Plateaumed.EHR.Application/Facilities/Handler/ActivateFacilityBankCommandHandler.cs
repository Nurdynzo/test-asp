using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    internal class ActivateFacilityBankCommandHandler : IActivateFacilityBankCommandHandler
    {
        private readonly IRepository<FacilityBank, long> _facilityBankRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ActivateFacilityBankCommandHandler(
            IRepository<FacilityBank, long> facilityBankRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _facilityBankRepository = facilityBankRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<FacilityBank> Handle(ActivateBankRequest request)
        {
            var facilityBank = await _facilityBankRepository.FirstOrDefaultAsync((long)request.Id);
            if (facilityBank == null)
            {
                throw new UserFriendlyException("Facility bank details cannot be found");
            }
            facilityBank.IsActive = request.IsActive;

            await _facilityBankRepository.UpdateAsync(facilityBank);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return facilityBank;
        }
    }
}
