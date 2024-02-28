using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetPatientMenstrualFlowQueryHandler : IGetPatientMenstrualFlowQueryHandler
    {
        private readonly IRepository<PatientMenstrualFlow, long> _patientMenstrualFlowRepository;
        public GetPatientMenstrualFlowQueryHandler(IRepository<PatientMenstrualFlow, long> patientMenstrualFlowRepository)
        {
            _patientMenstrualFlowRepository = patientMenstrualFlowRepository;
        }
        public async Task<List<GetPatientMenstrualFlowResponse>> Handle(long patientId)
        {
            var query = await (from p in _patientMenstrualFlowRepository.GetAll()
                               where p.PatientId == patientId
                               select new GetPatientMenstrualFlowResponse
                               {
                                   IsPeriodHeavierThanUsual = p.IsPeriodHeavierThanUsual,
                                   IsBloodClotLargerThanRegular = p.IsBloodClotLargerThanRegular,
                                   SanitaryPadUsedPerDay = p.SanitaryPadUsedPerDay,
                                   IsHeavyPeriodImpactDayToDayLife = p.IsHeavyPeriodImpactDayToDayLife,
                                   IsFlowFloodThroughSanitaryTowel = p.IsFlowFloodThroughSanitaryTowel,
                                   FlowType = p.FlowType,
                                   PatientId = p.PatientId

                               }).ToListAsync().ConfigureAwait(false);
            return query;
        }
    }
}
