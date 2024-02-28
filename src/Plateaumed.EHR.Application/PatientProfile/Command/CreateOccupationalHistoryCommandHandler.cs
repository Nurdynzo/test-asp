using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class CreateOccupationalHistoryCommandHandler : ICreateOccupationalHistoryCommandHandler
    {
        private readonly IRepository<OccupationalHistory, long> _occupationalHistoryRepository;
        private readonly IObjectMapper _mapper;

        public CreateOccupationalHistoryCommandHandler(IRepository<OccupationalHistory, long> occupationalHistoryRepository,
            IObjectMapper mapper)
        {
            _occupationalHistoryRepository = occupationalHistoryRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateOccupationalHistoryDto request)
        {
            var occupationHistory = _mapper.Map<OccupationalHistory>(request);
            await _occupationalHistoryRepository.InsertAsync(occupationHistory).ConfigureAwait(false);
        }
    }
}
