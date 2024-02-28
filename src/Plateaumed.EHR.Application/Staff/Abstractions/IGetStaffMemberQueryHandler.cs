using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Abstractions;

public interface IGetStaffMemberQueryHandler : ITransientDependency
{
    Task<GetStaffMemberForEditResponse> Handle(EntityDto<long> input);
}