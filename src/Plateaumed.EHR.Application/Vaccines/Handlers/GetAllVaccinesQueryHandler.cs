using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Vaccines.Abstractions;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines.Handlers
{
    public class GetAllVaccinesQueryHandler : IGetAllVaccinesQueryHandler
    {
        private readonly IRepository<Vaccine, long> _vaccineRepository;
        private readonly IObjectMapper _objectMapper;

        public GetAllVaccinesQueryHandler(IRepository<Vaccine, long> vaccineRepository, IObjectMapper objectMapper)
        {
            _vaccineRepository = vaccineRepository;
            _objectMapper = objectMapper;
        }

        public async Task<List<GetAllVaccinesResponse>> Handle()
        {
            return await _vaccineRepository.GetAll()
                .Select(x => _objectMapper.Map<GetAllVaccinesResponse>(x))
                .ToListAsync();
        }
    }
}
