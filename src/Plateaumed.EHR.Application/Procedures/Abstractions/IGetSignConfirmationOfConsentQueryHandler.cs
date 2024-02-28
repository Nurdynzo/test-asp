using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Runtime.Session;
using Plateaumed.EHR.Procedures.Dtos;

namespace Plateaumed.EHR.Procedures.Abstractions;

public interface IGetSignConfirmationOfConsentQueryHandler : ITransientDependency
{
    Task<SignConfirmationOfConsentDto> Handle(long procedureId, IAbpSession abpSession);
}