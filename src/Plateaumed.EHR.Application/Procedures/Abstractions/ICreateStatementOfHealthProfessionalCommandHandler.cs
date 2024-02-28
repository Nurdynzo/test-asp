using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Runtime.Session;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions;

public interface ICreateStatementOfHealthProfessionalCommandHandler : ITransientDependency
{
    Task Handle(CreateStatementOfHealthProfessionalDto requestDto, IAbpSession abpSession);
}