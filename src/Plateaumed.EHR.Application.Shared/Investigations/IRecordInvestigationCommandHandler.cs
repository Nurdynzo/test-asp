using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations;

public interface IRecordInvestigationCommandHandler : ITransientDependency
{
    Task Handle(RecordInvestigationRequest request, long facilityId);
}