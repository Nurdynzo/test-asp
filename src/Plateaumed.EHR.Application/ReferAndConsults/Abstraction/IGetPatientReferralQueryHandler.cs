using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.ReferAndConsults.Dtos;

namespace Plateaumed.EHR.ReferAndConsults.Abstraction;

public interface IGetPatientReferralQueryHandler : ITransientDependency
{
    Task<ReferralOrConsultReturnDto> Handle(long encounterId);
}