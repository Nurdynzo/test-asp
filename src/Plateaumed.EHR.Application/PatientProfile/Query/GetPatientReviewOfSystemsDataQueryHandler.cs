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
    public class GetPatientReviewOfSystemsDataQueryHandler : IGetPatientReviewOfSystemsDataQueryHandler
    {
        private readonly IRepository<ReviewOfSystem, long> _reviewOfSystemsRepository;
        private readonly IObjectMapper _mapper;
        public GetPatientReviewOfSystemsDataQueryHandler(IRepository<ReviewOfSystem, long> reviewOfSystemsRepository,
            IObjectMapper mapper)
        {
            _mapper = mapper;
            _reviewOfSystemsRepository = reviewOfSystemsRepository;
        }

        public async Task<List<GetPatientReviewOfSystemsDataResponseDto>> Handle(long patientId)
        {
             return await _reviewOfSystemsRepository.GetAll().Where(x => x.PatientId == patientId)
                .Select(x => _mapper.Map<GetPatientReviewOfSystemsDataResponseDto>(x))
                .ToListAsync().ConfigureAwait(false);
        }

    }
}
