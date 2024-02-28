using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.BedMaking.Dtos;

namespace Plateaumed.EHR.BedMaking.Abstractions;

public interface IGetPatientBedMakingSummaryQueryHandler : ITransientDependency
{
    Task<List<PatientBedMakingSummaryForReturnDto>> Handle(int patientId, int? tenantId);
}