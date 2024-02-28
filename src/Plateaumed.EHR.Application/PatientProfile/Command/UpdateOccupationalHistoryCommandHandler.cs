using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdateOccupationalHistoryCommandHandler : IUpdateOccupationalHistoryCommandHandler
    {
        private readonly IRepository<OccupationalHistory, long> _occupationalHistoryRepository;

        public UpdateOccupationalHistoryCommandHandler(IRepository<OccupationalHistory, long> occupationalHistoryRepository)
        {
            _occupationalHistoryRepository = occupationalHistoryRepository;
        }

        public async Task Handle(CreateOccupationalHistoryDto request)
        {
            if(request.Id != null && request.Id != 0)
            {
                var history = await _occupationalHistoryRepository.GetAll().SingleOrDefaultAsync(x => x.Id == request.Id)
                    ?? throw new UserFriendlyException("History not found");
                history.Note = request.Note ?? history.Note;
                history.IsCurrent = request.IsCurrent;
                history.From = request.From;
                history.To = request.To;
                history.WorkLocation = request.WorkLocation ?? history.WorkLocation;
                history.Occupation = request.Occupation ?? history.Occupation;
                await _occupationalHistoryRepository.UpdateAsync(history).ConfigureAwait(false);
            }
        }
    }
}
