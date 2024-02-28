using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Bogus;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;
using Abp.ObjectMapping;

namespace Plateaumed.EHR.Facilities.Handler
{
    internal class ActivateFacilityDefaultBankCommandHandler : IActivateFacilityDefaultBankCommandHandler
    {
        private readonly IRepository<FacilityBank, long> _facilityBankRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IObjectMapper _objectMapper;

        public ActivateFacilityDefaultBankCommandHandler(
            IRepository<FacilityBank, long> facilityBankRepository,
            IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper)
        {
            _facilityBankRepository = facilityBankRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _objectMapper = objectMapper;
        }

        public async Task<FacilityBank> Handle(ActivateDefaultBankRequest request)
        {
            var existingBank = await _facilityBankRepository.FirstOrDefaultAsync((long)request.Id);

            if (existingBank == null)
            {
                throw new UserFriendlyException("Facility bank details cannot be found");
            }

            if (request.IsDefault)
            {
                var facilityBank = _facilityBankRepository
                    .GetAll()
                    .Where(fac => fac.FacilityId  == existingBank.FacilityId)
                    .ToList();

                facilityBank.ForEach(bank => bank.IsDefault = false);
            }
            existingBank.IsDefault = request.IsDefault;

            await _facilityBankRepository.UpdateAsync(existingBank);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return existingBank;
        }
    }
}
