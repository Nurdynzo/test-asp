using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Abstractions;

public interface IGetStaffMemberWithUnitAndLevelQueryHandler : ITransientDependency
{
    Task<GetStaffMemberResponse> Handle(EntityDto<long> input);
}
