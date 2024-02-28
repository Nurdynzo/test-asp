using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.VitalSigns.Handlers;

public class GetPatientVitalsQueryHandler : IGetPatientVitalsQueryHandler
{
    private readonly IRepository<PatientVital, long> _patientVitalRepository; 
    private readonly IObjectMapper _objectMapper;

    public GetPatientVitalsQueryHandler(IRepository<PatientVital, long> patientVitalRepository, IObjectMapper objectMapper)
    {
        _patientVitalRepository = patientVitalRepository;
        _objectMapper = objectMapper;
    }
    
    public async Task<List<PatientVitalResponseDto>> Handle(long patientId, long? procedureId = null)
    {
        var patientVitals = await _patientVitalRepository
            .GetAll()
            .Include(v => v.Patient)
            .Include(v => v.VitalSign)
            .Include(v => v.MeasurementSite)
            .Include(v => v.MeasurementRange)
            .Include(v => v.CreatorUser)
            .Include(v => v.LastModifierUser)
            .Where(v => v.PatientId == patientId && v.IsDeleted == false)
            .WhereIf(procedureId.HasValue, v => v.ProcedureId == procedureId.Value)
            .ToListAsync();
 
        return _objectMapper.Map<List<PatientVitalResponseDto>>(patientVitals);  
    }
}