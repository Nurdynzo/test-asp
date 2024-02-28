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
    public class GetPatientBloodTransfusionHistory : IGetPatientBloodTransfusionHistory
    {
        private readonly IRepository<BloodTransfusionHistory, long> _bloodTransfusionRepository;
        private readonly IObjectMapper _mapper;

        public GetPatientBloodTransfusionHistory(IRepository<BloodTransfusionHistory, long> bloodTransfusionRepository,
            IObjectMapper mapper)
        {
            _bloodTransfusionRepository = bloodTransfusionRepository;
            _mapper = mapper;
        }

        public async Task<List<GetPatientBloodTransfusionHistoryResponseDto>> Handle(long patientId)
        {
            return await _bloodTransfusionRepository.GetAll()
                .Where(x => x.PatientId == patientId)
                .Select(x => _mapper.Map<GetPatientBloodTransfusionHistoryResponseDto>(x))
                .ToListAsync();
        }
    }
}
