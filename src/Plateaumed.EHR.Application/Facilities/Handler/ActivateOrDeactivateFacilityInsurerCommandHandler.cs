using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Facilities.Abstractions;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Handler
{
    public class ActivateOrDeactivateFacilityInsurerCommandHandler : IActivateOrDeactivateFacilityInsurerCommandHandler
    {
        private readonly IRepository<FacilityInsurer, long> _facilityInsurerRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ActivateOrDeactivateFacilityInsurerCommandHandler(
            IRepository<FacilityInsurer, long> facilityInsurerRepository,
             IUnitOfWorkManager unitOfWorkManager)
        {
            _facilityInsurerRepository = facilityInsurerRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<FacilityInsurer> Handle(ActivateOrDeactivateFacilityInsurerRequest request)
        {
            var facilityInsurer = await _facilityInsurerRepository.FirstOrDefaultAsync((long)request.Id);
            if (facilityInsurer == null)
            {
                throw new UserFriendlyException("Facility Insurer cannot be found");
            }
            facilityInsurer.IsActive = request.IsActive;

            await _facilityInsurerRepository.UpdateAsync(facilityInsurer);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return facilityInsurer;
        }
    }
}
