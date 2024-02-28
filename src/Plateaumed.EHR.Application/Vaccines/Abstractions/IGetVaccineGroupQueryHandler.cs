using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines.Abstractions;

public interface IGetVaccineGroupQueryHandler : ITransientDependency
{
    Task<GetVaccineGroupResponse> Handle(EntityDto<long> request);
}