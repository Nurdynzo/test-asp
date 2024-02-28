using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Diagnoses.Abstraction;
using Plateaumed.EHR.Diagnoses.Dto;

namespace Plateaumed.EHR.Diagnoses.Query
{
    public class GetPatientDiagnosisWithEncounterQueryHandler : IGetPatientDiagnosisWithEncounterQueryHandler
    {
        private readonly IBaseQuery _baseQuery;

        public GetPatientDiagnosisWithEncounterQueryHandler(IBaseQuery baseQuery)
        {
            _baseQuery = baseQuery;
        }
        public async Task<List<DiagnosisDto>> Handle(long patientId, long encounterId)
        {
            //Get Patient Diagnosis
            return await _baseQuery.GetPatientDiagnosisWithEncounterId(patientId, encounterId)
                .Select(s => new DiagnosisDto()
                {
                    TenantId = s.TenantId,
                    PatientId = s.PatientId,
                    EncounterId = s.EncounterId,
                    Sctid = s.Sctid,
                    Description = s.Description,
                    Notes = s.Notes,
                    Status = s.Status,
                    CreatedAt = s.CreationTime,
                    CreatorUserId = s.CreatorUserId ?? 0
                })
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }


    }
}