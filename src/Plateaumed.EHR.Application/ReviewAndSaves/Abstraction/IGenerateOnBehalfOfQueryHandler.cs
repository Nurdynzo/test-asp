using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Abstraction;

public interface IGenerateOnBehalfOfQueryHandler : ITransientDependency
{
    Task<List<GetStaffMembersSimpleResponseDto>> Handle(long userId, long facilityId);
}
