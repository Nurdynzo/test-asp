using Abp.Dependency;
using Plateaumed.EHR.Facilities.Dtos;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Facilities.Abstractions
{
    public interface IUpdateFacilityBankCommandHandler : ITransientDependency
    {
        Task Handle(CreateOrEditBankRequest request);
    }
}
