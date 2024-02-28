using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Runtime.Session;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions;

public interface IScheduleProcedureCommandHandler : ITransientDependency
{
    Task Handle(ScheduleProcedureDto requestDto, IAbpSession abpSession);
}