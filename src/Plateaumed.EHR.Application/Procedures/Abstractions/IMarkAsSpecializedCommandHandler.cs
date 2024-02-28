using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Runtime.Session;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions;

public interface IMarkAsSpecializedCommandHandler : ITransientDependency
{
    Task Handle(CreateSpecializedProcedureDto requestDto, IAbpSession abpSession);
}