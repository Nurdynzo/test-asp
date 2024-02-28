using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions;

public interface ICreateAnaesthesiaNoteCommandHandler : ITransientDependency
{
    Task Handle(CreateAnaesthesiaNoteDto requestDto);
}