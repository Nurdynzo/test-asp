using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class CreateFacilityBankCommandHandler : ICreateFacilityBankCommandHandler
    {
        private readonly IRepository<FacilityBank, long> _facilityBanksRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IObjectMapper _objectMapper;
        private readonly IAbpSession _abpSession;

        public CreateFacilityBankCommandHandler(
            IRepository<FacilityBank, long> facilitybankdetailsRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IObjectMapper objectMapper,
            IAbpSession abpSession) 
        {
            _facilityBanksRepository = facilitybankdetailsRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _objectMapper = objectMapper;
            _abpSession = abpSession;
        }

        public async Task<FacilityBank> Handle(CreateOrEditBankRequest request)
        {
            var facilityBank = _objectMapper.Map<FacilityBank>(request);

            if (_abpSession.TenantId != null)
            {
                facilityBank.TenantId = (int)_abpSession.TenantId;
            }

            facilityBank.BankAccountHolder = request.BankAccountHolder;
            facilityBank.BankAccountNumber = request.BankAccountNumber;
            facilityBank.BankName = request.BankName;
            facilityBank.IsActive = request.IsActive;

            await _facilityBanksRepository.InsertAsync(facilityBank);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return facilityBank;
        }
    }
}
