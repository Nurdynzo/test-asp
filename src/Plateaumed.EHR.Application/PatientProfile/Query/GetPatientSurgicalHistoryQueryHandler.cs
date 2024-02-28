using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetPatientSurgicalHistoryQueryHandler : IGetPatientSurgicalHistoryQueryHandler
    {
        private readonly IRepository<SurgicalHistory, long> _surgicalHistoryRepository;
        private readonly IObjectMapper _mapper;

        public GetPatientSurgicalHistoryQueryHandler(IRepository<SurgicalHistory, long> surgicalHistoryRepository,
            IObjectMapper mapper)
        {
            _surgicalHistoryRepository = surgicalHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<GetSurgicalHistoryResponseDto>> Handle(long patientId)
        {
            var result = await _surgicalHistoryRepository.GetAll()
                .Where(x => x.PatientId == patientId)
                .Select(x => _mapper.Map<GetSurgicalHistoryResponseDto>(x))
                .ToListAsync();
            return result;
        }
    }
}
