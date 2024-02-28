using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Microsoft.AspNetCore.Identity;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Abstractions;

public interface ICreateStaffMemberCommandHandler : ITransientDependency
{
    Task<User> Handle(CreateOrEditStaffMemberRequest request, Action<IdentityResult> checkErrors);
}