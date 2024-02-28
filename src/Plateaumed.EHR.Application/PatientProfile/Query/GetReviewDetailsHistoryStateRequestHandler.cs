using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetReviewDetailsHistoryStateRequestHandler : IGetReviewDetailsHistoryStateRequestHandler
    {
        private readonly IRepository<ReviewDetailsHistoryState, long> _reviewDetailsHistoryStateRepository;
        private readonly IObjectMapper _mapper;
        public GetReviewDetailsHistoryStateRequestHandler(
            IRepository<ReviewDetailsHistoryState, long> reviewDetailsHistoryStateRepository,
            IObjectMapper mapper)
        {
            _reviewDetailsHistoryStateRepository = reviewDetailsHistoryStateRepository;
            _mapper = mapper;
        }


        public async Task<ReviewDetailsHistoryStateDto> Handle(long patientId)
        {
            var states = await _reviewDetailsHistoryStateRepository.GetAll().Where(x => x.PatientId == patientId)
                .SingleOrDefaultAsync().ConfigureAwait(false);
            return _mapper.Map<ReviewDetailsHistoryStateDto>(states);
        }
    }
}
