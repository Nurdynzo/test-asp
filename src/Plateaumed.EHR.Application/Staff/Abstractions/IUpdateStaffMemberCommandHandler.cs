using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff.Abstractions;

public interface IUpdateStaffMemberCommandHandler : ITransientDependency
{
    Task Handle(CreateOrEditStaffMemberRequest request);
}