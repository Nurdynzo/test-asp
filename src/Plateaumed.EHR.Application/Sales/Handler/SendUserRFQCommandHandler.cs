using Abp.Domain.Repositories;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Net.Emailing.ContactSales;
//using Plateaumed.EHR.Sales.Dto;
using System.Threading.Tasks;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Sales.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.Sales.Handler
{
    public class SendUserRFQCommandHandler : EHRAppServiceBase, ISendUserRFQCommandHandler
    {
        private IUserEmailer _userEmailer;
        private readonly IRepository<Facility, long> _facilityRepository;
        private readonly IRepository<User, long> _userRepository;

        public SendUserRFQCommandHandler(
            IUserEmailer userEmailer,
            IRepository<Facility, long> facilityRepository,
            IRepository<User, long> userRepository

            )
        {
            _userEmailer = userEmailer;
            _facilityRepository = facilityRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(QuotationRequest input)
        {

            var tenantId = (int)AbpSession.TenantId;
            var userId = (int)AbpSession.UserId;
            var facility = await _facilityRepository.FirstOrDefaultAsync(x => x.TenantId == tenantId);
            if (facility == null)
            {
                return;
            }
            var user = await _userRepository.FirstOrDefaultAsync(x => x.Id == userId);
            var quote = ObjectMapper.Map<QuotationRequest>(input);

            await _userEmailer.SendRFQDetailsToUsers(facility, quote, user.FullName);
        }
    }
}
