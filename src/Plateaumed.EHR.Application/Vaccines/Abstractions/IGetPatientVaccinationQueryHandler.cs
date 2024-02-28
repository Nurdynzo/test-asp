using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines.Abstractions;

public interface IGetPatientVaccinationQueryHandler : ITransientDependency
{ 
    Task<List<VaccinationResponseDto>> Handle(EntityDto<long> patientId, long? procedureId = null);
}