using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdatePatientImplantCommandHandler : IUpdatePatientImplantCommandHandler
    {
        private readonly IRepository<PatientImplant, long> _implantRepository;

        public UpdatePatientImplantCommandHandler(IRepository<PatientImplant, long> implantRepository)
        {
            _implantRepository = implantRepository;
        }

        public async Task Handle(CreatePatientImplantCommandRequestDto request)
        {
            var history = await _implantRepository.GetAll().SingleOrDefaultAsync(x => x.Id == request.Id)
                ?? throw new UserFriendlyException("History not found");

            history.Note = request.Note ?? history.Note;
            history.Name = request.Name ?? history.Name;
            history.SnomedId = request.SnomedId;
            history.IsIntact = request.IsIntact;
            history.HasComplications = request.HasComplications;
            history.DateInserted = request.DateInserted;
            history.DateRemoved = request.DateRemoved;

            await _implantRepository.UpdateAsync(history).ConfigureAwait(false);
        }
    }
}
