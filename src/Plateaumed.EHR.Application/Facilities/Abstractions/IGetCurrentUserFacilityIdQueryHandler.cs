using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.Facilities.Abstractions;

public interface IGetCurrentUserFacilityIdQueryHandler: ITransientDependency
{
    Task<long?> Handle(long userId);
    Task<long?> Handle();
}