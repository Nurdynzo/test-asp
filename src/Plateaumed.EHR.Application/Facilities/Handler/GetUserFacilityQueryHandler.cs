using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using Plateaumed.EHR.MultiTenancy;
using Plateaumed.EHR.Staff;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class GetUserFacilityQueryHandler : IGetUserFacilityQueryHandler
    {
        private readonly IRepository<Facility, long> _facilityRepository;
        private readonly IRepository<JobTitle, long> _jobTitleRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly IAbpSession _abpSession;

        public GetUserFacilityQueryHandler(
            IRepository<Facility, long> facilityRepository,
            IRepository<JobTitle, long> jobTitleRepository,
            IRepository<User, long> userRepository,
            IObjectMapper objectMapper,
            IRepository<Tenant> tenantRepository,
            IAbpSession abpSession
            )

        {
            _facilityRepository = facilityRepository;
            _jobTitleRepository = jobTitleRepository;
            _userRepository = userRepository;
            _objectMapper = objectMapper;
            _tenantRepository = tenantRepository;
            _abpSession = abpSession;
        }

        public async Task<List<GetUserFacilityViewDto>> Handle(string emailAddress)
        {
            var user = await GetUser(emailAddress);
            var userFacility = await GetUserFacility(emailAddress);
            var tenant = await GetTenant();

            var resultDtos = userFacility.Select(userFacility =>
            {
                var userFacilityDto = _objectMapper.Map<UserFacilityDto>(userFacility);
                userFacilityDto.IsEmailConfirmed = user.IsEmailConfirmed;
                userFacilityDto.IsActive = user.IsActive;
                userFacilityDto.TenancyName = tenant.TenancyName;

                var jobTitleDtos = GetUserFacilityJobTitles(userFacility.Id);

                return new GetUserFacilityViewDto
                {
                    UserFacility = userFacilityDto,
                    UserFacilityJobTitleDto = jobTitleDtos
                };
            }).ToList();

            return resultDtos;
        }

        private async Task<User> GetUser(string emailAddress)
        {
            return await _userRepository.FirstOrDefaultAsync(x => x.EmailAddress.Equals(emailAddress))
                ?? throw new UserFriendlyException("User does not exist");
        }

        private async Task<List<Facility>> GetUserFacility(string emailAddress)
        {
            return await _facilityRepository.GetAll()
                .Where(f => f.EmailAddress == emailAddress && f.IsActive)
                .ToListAsync()
                ?? throw new UserFriendlyException("Facility details cannot be found");
        }

        private async Task<Tenant> GetTenant()
        {
            return await _tenantRepository.FirstOrDefaultAsync(x => x.Id == _abpSession.TenantId)
                ?? throw new UserFriendlyException("User is not logged in to the facility");
        }

        private List<UserFacilityJobTitleDto> GetUserFacilityJobTitles(long facilityId)
        {
            return _jobTitleRepository.GetAll()
                .Where(jt => jt.FacilityId == facilityId)
                .Select(jt => new UserFacilityJobTitleDto
                {
                    Name = jt.Name,
                }).ToList();
        }

    }
}
