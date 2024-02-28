using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Command
{
    public class DeleteReviewOfSystemDataCommandHandler : IDeleteReviewOfSystemDataCommandHandler
    {
        private readonly IRepository<ReviewOfSystem, long> _reviewOfSystemsRepository;

        public DeleteReviewOfSystemDataCommandHandler(IRepository<ReviewOfSystem, long> reviewOfSystemsRepository)
        {
            _reviewOfSystemsRepository = reviewOfSystemsRepository;   
        }

        public async Task Handle(long id)
        {
            var history = await _reviewOfSystemsRepository.GetAll().SingleOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false) ?? throw new UserFriendlyException("Data not found");
            await _reviewOfSystemsRepository.DeleteAsync(history).ConfigureAwait(false);
        }
    }
}
