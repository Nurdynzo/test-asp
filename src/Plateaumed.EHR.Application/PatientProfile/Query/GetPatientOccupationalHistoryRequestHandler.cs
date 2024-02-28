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
    public class GetPatientOccupationalHistoryRequestHandler : IGetPatientOccupationalHistoryRequestHandler
    {
        private readonly IRepository<OccupationalHistory, long> _occupationalHistoryRepository;
        private readonly IObjectMapper _mapper;

        public GetPatientOccupationalHistoryRequestHandler(IRepository<OccupationalHistory, long> occupationalHistoryRepository,
            IObjectMapper mapper)
        {
            _occupationalHistoryRepository = occupationalHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CreateOccupationalHistoryDto>> Handle(long patientId)
        {
            var result = await _occupationalHistoryRepository.GetAll()
                .Where(x => x.PatientId == patientId).ToListAsync()
                .ConfigureAwait(false);
            return _mapper.Map<List<CreateOccupationalHistoryDto>>(result);
        }
    }
}
