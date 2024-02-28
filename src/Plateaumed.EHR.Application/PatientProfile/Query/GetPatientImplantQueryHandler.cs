using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetPatientImplantQueryHandler : IGetPatientImplantQueryHandler
    {
        private readonly IRepository<PatientImplant, long> _patientImplantRepository;

        public GetPatientImplantQueryHandler(IRepository<PatientImplant, long> patientImplantRepository)
        {
            _patientImplantRepository = patientImplantRepository;
        }

        public async Task<List<GetPatientImplantResponseDto>> Handle(long patientId)
        {
            var patientImplants = await _patientImplantRepository.GetAll()
                .Where(x => x.PatientId == patientId)
                .Select(x => new GetPatientImplantResponseDto 
                {
                    PatientId = x.PatientId,
                    Name = x.Name,
                    SnomedId = x.SnomedId,
                    IsIntact = x.IsIntact,
                    HasComplications = x.HasComplications,
                    Note = x.Note,
                    DateInserted = x.DateInserted,
                    DateRemoved = x.DateRemoved,
                    CreatorUserId = x.CreatorUserId.GetValueOrDefault(),
                    Id = x.Id
                }).ToListAsync();
            return patientImplants;
        }
    }
}
