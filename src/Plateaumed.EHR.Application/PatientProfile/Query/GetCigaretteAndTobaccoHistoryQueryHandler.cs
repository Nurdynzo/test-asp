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
    public class GetCigaretteAndTobaccoHistoryQueryHandler : IGetCigaretteAndTobaccoHistoryQueryHandler
    {
        private readonly IRepository<CigeretteAndTobaccoHistory, long> _cigaretteAndTobaccoHistory;
        private readonly IObjectMapper _mapper;

        public GetCigaretteAndTobaccoHistoryQueryHandler(IRepository<CigeretteAndTobaccoHistory, long> cigaretteAndTobaccoHistory,
            IObjectMapper mapper)
        {
            _cigaretteAndTobaccoHistory = cigaretteAndTobaccoHistory;
            _mapper = mapper;
        }


        public async Task<List<GetCigaretteHistoryResponseDto>> Handle(long patientId)
        {
            return await _cigaretteAndTobaccoHistory.GetAll().Where(c => c.PatientId == patientId)
                .Select(x => _mapper.Map<GetCigaretteHistoryResponseDto>(x))
                .ToListAsync();
        }
    }
}
