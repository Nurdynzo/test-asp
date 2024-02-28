using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.WoundDressing.Dtos;

namespace Plateaumed.EHR.WoundDressing.Abstractions;

public interface IGetPatientWoundDressingSummaryQueryHandler : ITransientDependency
{
    Task<List<WoundDressingSummaryForReturnDto>> Handle(int patientId);
}