using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdateRecreationalDrugHistoryCommandHandler : IUpdateRecreationalDrugHistoryCommandHandler
    {
        private readonly IRepository<RecreationalDrugHistory, long> _recreationDrugHistoryRepository;

        public UpdateRecreationalDrugHistoryCommandHandler(IRepository<RecreationalDrugHistory, long> recreationDrugHistoryRepository)
        {
            _recreationDrugHistoryRepository = recreationDrugHistoryRepository;
        }

        public async Task Handle(CreateRecreationalDrugsHistoryRequestDto request)
        {
            var existingHistory = await _recreationDrugHistoryRepository.GetAll()
                .SingleOrDefaultAsync(x => x.Id == request.Id) ?? throw new UserFriendlyException("History not found");

            existingHistory.PatientDoesNotTakeRecreationalDrugs = request.PatientDoesNotTakeRecreationalDrugs;
            existingHistory.DrugUsed = request.DrugUsed ?? existingHistory.DrugUsed;
            existingHistory.Route = request.Route ?? existingHistory.Route;
            existingHistory.StillUsingDrugs = request.StillUsingDrugs;
            existingHistory.From = request.From;
            existingHistory.To = request.To;
            existingHistory.Note = request.Note ?? existingHistory.Note;
            await _recreationDrugHistoryRepository.UpdateAsync(existingHistory);
       
        }
    }
}
