using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Abstractions
{
    public interface IActivateOrDeactivateFacilityInsurerCommandHandler : ITransientDependency
    {
        Task<FacilityInsurer> Handle(ActivateOrDeactivateFacilityInsurerRequest request);
    }
}
