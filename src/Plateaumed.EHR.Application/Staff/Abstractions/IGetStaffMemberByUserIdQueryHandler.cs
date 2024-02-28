using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Abstractions;

public interface IGetStaffMemberByUserIdQueryHandler : ITransientDependency
{
    Task<User> Handle(long userId);
}