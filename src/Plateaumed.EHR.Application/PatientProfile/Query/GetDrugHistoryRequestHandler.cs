using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetDrugHistoryRequestHandler : IGetDrugHistoryRequestHandler
    {
        private readonly IRepository<DrugHistory, long> _drugHistoryRepository;
        private readonly IObjectMapper _mapper;

        public GetDrugHistoryRequestHandler(IRepository<DrugHistory, long> drugHistoryRepository,
            IObjectMapper mapper)
        {
            _drugHistoryRepository = drugHistoryRepository;
            _mapper = mapper;
        }

        public async Task<List<GetDrugHistoryResponseDto>> Handle(long patientId)
        {
            return await _drugHistoryRepository.GetAll().Where(h => h.PatientId == patientId)
                .Select(h => _mapper.Map<GetDrugHistoryResponseDto>(h))
                .ToListAsync();
        }
    }
}
