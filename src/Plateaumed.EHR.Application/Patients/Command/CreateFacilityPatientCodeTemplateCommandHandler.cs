using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Patients.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Patients.Command
{
    public class CreateFacilityPatientCodeTemplateCommandHandler : ICreateFacilityPatientCodeTemplateCommandHandler
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<Facility, long> _facilityRepository;

        public CreateFacilityPatientCodeTemplateCommandHandler(
            IUnitOfWorkManager unitOfWorkManager,
            IObjectMapper objectMapper,
            IRepository<Facility, long> facilityRepository)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _objectMapper = objectMapper;
            _facilityRepository = facilityRepository;
        }

        public async Task Handle(CreateOrEditPatientCodeTemplateDto request)
        {
            var patientCodeTemplate = _objectMapper.Map<PatientCodeTemplate>(request);

            var facility = await _facilityRepository.FirstOrDefaultAsync(request.FacilityId);

            if (facility == null)
            {
                throw new UserFriendlyException("Facility not found");
            }

            facility.PatientCodeTemplate = patientCodeTemplate;

            await _facilityRepository.UpdateAsync(facility);

            await _unitOfWorkManager.Current.SaveChangesAsync();
        }
    }
}
