using Abp.Domain.Repositories;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Net.Emailing.ContactSales;
//using Plateaumed.EHR.Sales.Dto;
using System.Threading.Tasks;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Sales.Abstractions;
using Microsoft.EntityFrameworkCore;
using Abp.UI;

namespace Plateaumed.EHR.Sales.Handler
{
    public class SendSalesRFQCommandHandler : EHRAppServiceBase, ISendSalesRFQCommandHandler
    {
        private readonly IUserEmailer _userEmailer;
        private readonly IRepository<Facility, long> _facilityRepository;
        private readonly IRepository<User, long> _userRepository;

        public SendSalesRFQCommandHandler(
            IUserEmailer userEmailer,
            IRepository<Facility, long> facilityRepository,
            IRepository<User, long> userRepository
            )
        {
            _userEmailer = userEmailer;
            _facilityRepository = facilityRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(QuotationRequest request)
        {
            var tenantId = (int)AbpSession.TenantId;
            var userId = (int)AbpSession.UserId;
            var facility = await _facilityRepository.GetAll()
                .Include(f => f.TypeFk)
                .FirstOrDefaultAsync(x => x.TenantId == tenantId) ?? throw new UserFriendlyException("Facility cannot be found");
            var user = await _userRepository.GetAll()
                .Include(r => r.Roles)
                .FirstOrDefaultAsync(x => x.TenantId == tenantId && x.Id == userId);
            var quote = ObjectMapper.Map<QuotationRequest>(request);

            await _userEmailer.SendRFQToSalesTeam(facility, quote, user.FullName);
        }
    }
}
