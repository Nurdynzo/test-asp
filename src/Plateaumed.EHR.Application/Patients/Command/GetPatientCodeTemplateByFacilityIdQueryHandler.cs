using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Patients.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Patients.Command
{
    public class GetPatientCodeTemplateByFacilityIdQueryHandler : IGetPatientCodeTemplateByFacilityIdQueryHandler
    {
        private readonly IRepository<Facility, long> _facilityRepository;
        private readonly IObjectMapper _objectMapper;
        public GetPatientCodeTemplateByFacilityIdQueryHandler(
             IRepository<Facility, long> facilityRepository,
             IObjectMapper objectMapper)
        {
            _facilityRepository = facilityRepository;
            _objectMapper = objectMapper;
        }

        public async Task<PatientCodeTemplateDto> Handle(long facilityId)
        {
            var facility = await _facilityRepository
                           .GetAll()
                           .Include(f => f.PatientCodeTemplate)
                           .Where(f => f.Id == facilityId)
                           .FirstOrDefaultAsync();

            if (facility == null)
            {
                throw new UserFriendlyException("Facility not found");
            }

            var patientCodeTemplateDto = _objectMapper.Map<PatientCodeTemplateDto>(facility.PatientCodeTemplate);

            return patientCodeTemplateDto;
        }
    }
}
