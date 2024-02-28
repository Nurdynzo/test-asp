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
    public class GetRecreationalDrugsHistoryRequestHandler : IGetRecreationalDrugsHistoryRequestHandler
    {
        private readonly IRepository<RecreationalDrugHistory, long> _recreationalDrugsRepository;
        private readonly IObjectMapper _mapper;

        public GetRecreationalDrugsHistoryRequestHandler(IRepository<RecreationalDrugHistory, long> recreationalDrugsRepository,
            IObjectMapper mapper)
        {
            _recreationalDrugsRepository = recreationalDrugsRepository;
            _mapper = mapper;
        }

        public async Task<List<GetRecreationalDrugHistoryResponseDto>> Handle(long patientId)
        {
            return await _recreationalDrugsRepository.GetAll().Where(x => x.PatientId == patientId)
                .Select(x => _mapper.Map<GetRecreationalDrugHistoryResponseDto>(x)).ToListAsync();
        }
    }
}
