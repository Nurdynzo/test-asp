using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdateAlcoholHistoryCommandHandler : IUpdateAlcoholHistoryCommandHandler
    {
        private readonly IRepository<AlcoholHistory, long> _alcoholHistoryRepository;

        public UpdateAlcoholHistoryCommandHandler(IRepository<AlcoholHistory, long> alcoholRepository)
        {
            _alcoholHistoryRepository = alcoholRepository;
        }

        public async Task Handle(CreateAlcoholHistoryRequestDto request)
        {
            var history = await _alcoholHistoryRepository.GetAll().SingleOrDefaultAsync(x => x.Id == request.Id)
                ?? throw new UserFriendlyException("History not found");
            history.Frequency = request.Frequency;
            history.Interval = request.Interval;
            history.Note = request.Note ?? history.Note;
            history.TypeOfAlcohol = request.TypeOfAlcohol ?? request.TypeOfAlcohol;
            history.MaximumAmountOfUnits = request.MaximumAmountOfUnits;
            history.DetailsOfAlcoholIntakeNotKnown = request.DetailsOfAlcoholIntakeNotKnown;
            history.DoesNotTakeAlcohol = request.DoesNotTakeAlcohol;
            history.MaximumUnitTaken = request.MaximumUnitTaken ?? history.MaximumUnitTaken;
            await _alcoholHistoryRepository.UpdateAsync(history);
        }
    }
}
