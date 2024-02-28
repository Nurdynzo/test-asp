using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetPatientMenstrualFrequencyQueryHandler : IGetPatientMenstrualFrequencyQueryHandler
    {
        private readonly IRepository<PatientMensurationDuration, long> _mensurationDurationRepository;
        public GetPatientMenstrualFrequencyQueryHandler(IRepository<PatientMensurationDuration, long> mensurationDurationRepository)
        {
            _mensurationDurationRepository = mensurationDurationRepository;
        }
        public async Task<List<GetPatientMenstrualFrequencyResponse>> Handle(long patientId)
        {
            var query = await (from p in _mensurationDurationRepository.GetAll()
                               where p.PatientId == patientId
                               select new GetPatientMenstrualFrequencyResponse
                               {
                                   LastDayOfPeriod = p.LastDayOfPeriod,
                                   AveragePeriodDuration = p.AveragePeriodDuration,
                                   IsPeriodPredictable = p.IsPeriodPredictable,
                                   AverageCycleLength = p.AverageCycleLength,
                                   AverageCycleLengthUnit = p.AverageCycleLengthUnit,
                                   RequestedTest = p.RequestedTest,
                                   Notes = p.Notes,
                                   PatientId = p.PatientId

                               }).ToListAsync().ConfigureAwait(false);
            return query;
        }
    }
}
