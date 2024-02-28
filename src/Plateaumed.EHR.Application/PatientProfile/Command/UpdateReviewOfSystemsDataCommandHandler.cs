using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class UpdateReviewOfSystemsDataCommandHandler : IUpdateReviewOfSystemsDataCommandHandler
    {
        private readonly IRepository<ReviewOfSystem, long> _reviewOfSystemsRepository;

        public UpdateReviewOfSystemsDataCommandHandler(IRepository<ReviewOfSystem, long> reviewOfSystemsRepository)
        {
            _reviewOfSystemsRepository = reviewOfSystemsRepository;
        }

        public async Task Handle(CreateReviewOfSystemsRequestDto request)
        {
            var history = await _reviewOfSystemsRepository.GetAll().SingleOrDefaultAsync(x => x.Id == request.Id)
                .ConfigureAwait(false) ?? throw new UserFriendlyException("Data not found");
            history.Name = request.Name ?? history.Name;
            history.SnomedId = request.SnomedId;
            history.Category = request.Category;
            await _reviewOfSystemsRepository.UpdateAsync(history).ConfigureAwait(false);
        }
    }
}
