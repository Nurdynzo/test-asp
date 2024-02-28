using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.ReferAndConsults.Dtos;

namespace Plateaumed.EHR.ReferAndConsults.Abstraction;

public interface IGetReferralLetterQueryHandler : ITransientDependency
{
    Task<ReferralReturnDto> Handle(ReferralRequestDto request, User loginUser);
}
