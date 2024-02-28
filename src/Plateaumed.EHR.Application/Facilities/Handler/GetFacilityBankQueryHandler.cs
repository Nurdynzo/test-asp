using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Plateaumed.EHR.Facilities.Handler
{
    internal class GetFacilityBankQueryHandler : IGetFacilityBankQueryHandler
    {
        private readonly IRepository<FacilityBank, long> _facilityBankRepository;
        private readonly IObjectMapper _objectMapper;

        public GetFacilityBankQueryHandler(
            IRepository<FacilityBank, long> facilityBankRepository,
            IObjectMapper objectMapper
            )
        {
            _facilityBankRepository = facilityBankRepository;
            _objectMapper = objectMapper;
        }

        public async Task<FacilityBankResponseDto> Handle(EntityDto<long> request)
        {
            var existingBanks = await _facilityBankRepository.FirstOrDefaultAsync(request.Id);

            if (existingBanks == null)
            {
                throw new UserFriendlyException("Facility bank details cannot found");
            }

            var banks = _objectMapper.Map<FacilityBankResponseDto>(existingBanks);

            return banks;
        }
    }
}
