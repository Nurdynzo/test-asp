using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Runtime.Session;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions;

public interface ISignConfirmationOfConsentCommandHandler : ITransientDependency
{
    Task Handle(SignConfirmationOfConsentDto requestDto, IAbpSession abpSession);
}