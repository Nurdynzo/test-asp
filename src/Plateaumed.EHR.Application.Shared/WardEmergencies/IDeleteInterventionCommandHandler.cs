using System.Threading.Tasks;
using Abp.Dependency;
namespace Plateaumed.EHR.WardEmergencies
{
    public interface IDeleteInterventionCommandHandler: ITransientDependency
    {
        Task Handle(long interventionId);
    }
}
