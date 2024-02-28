using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines.Abstractions;

public interface IGetVaccineQueryHandler : ITransientDependency
{
    Task<GetVaccineResponse> Handle(EntityDto<long> request);
}