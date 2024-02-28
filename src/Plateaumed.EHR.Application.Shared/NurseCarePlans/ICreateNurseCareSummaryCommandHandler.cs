using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.NurseCarePlans.Dto;

namespace Plateaumed.EHR.NurseCarePlans;

public interface ICreateNurseCareSummaryCommandHandler : ITransientDependency
{
    Task Handle(CreateNurseCarePlanRequest request);
}