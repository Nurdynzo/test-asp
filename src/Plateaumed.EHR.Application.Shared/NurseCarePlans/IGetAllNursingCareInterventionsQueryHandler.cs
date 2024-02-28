using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.NurseCarePlans.Dto;

namespace Plateaumed.EHR.NurseCarePlans;

public interface IGetAllNursingCareInterventionsQueryHandler : ITransientDependency
{
    Task<List<GetNurseCarePlansResponse>> Handle(List<long> outcomesIds);
}