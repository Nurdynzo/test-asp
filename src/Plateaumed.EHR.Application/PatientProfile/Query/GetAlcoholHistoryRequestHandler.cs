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
    public class GetAlcoholHistoryRequestHandler : IGetAlcoholHistoryRequestHandler
    {
        private readonly IRepository<AlcoholHistory, long> _alcoholHistoryRepository;
        private readonly IObjectMapper _mapper;

        public GetAlcoholHistoryRequestHandler(IRepository<AlcoholHistory, long> alcoholHistoryRepository,
            IObjectMapper mapper)
        {
            _alcoholHistoryRepository = alcoholHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAlcoholHistoryResponseDto>> Handle(long patientId)
        {
            return await _alcoholHistoryRepository.GetAll().Where(x => x.PatientId == patientId)
                .Select(x => _mapper.Map<GetAlcoholHistoryResponseDto>(x))
                .ToListAsync();
        }
    }
}
