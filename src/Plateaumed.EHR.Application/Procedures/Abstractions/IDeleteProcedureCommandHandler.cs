using System.Threading.Tasks;
using Abp.Dependency;
namespace Plateaumed.EHR.Procedures.Abstractions
{
    public interface IDeleteProcedureCommandHandler:ITransientDependency
    {
        Task Handle(long id);
    }
}
