using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class UpdateFacilityBankCommandHandler : IUpdateFacilityBankCommandHandler
    {
        private readonly IRepository<FacilityBank, long> _facilityBankRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UpdateFacilityBankCommandHandler(IRepository<FacilityBank, long> facilitybankdetailsRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _facilityBankRepository = facilitybankdetailsRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public  async Task Handle(CreateOrEditBankRequest request)
        {
            var facilityBank = await _facilityBankRepository.FirstOrDefaultAsync((long)request.Id);
            if (facilityBank == null)
            {
                throw new UserFriendlyException("Facility bank details cannot be found");
            }
            facilityBank.BankAccountHolder = request.BankAccountHolder;
            facilityBank.BankAccountNumber = request.BankAccountNumber;
            facilityBank.BankName = request.BankName;
            facilityBank.IsActive = request.IsActive;
            facilityBank.IsDefault = request.IsDefault;

            await _facilityBankRepository.UpdateAsync(facilityBank);
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }
    }
}
