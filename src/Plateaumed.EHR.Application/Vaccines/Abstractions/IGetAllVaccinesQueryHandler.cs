using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines.Abstractions;

public interface IGetAllVaccinesQueryHandler : ITransientDependency
{
    Task<List<GetAllVaccinesResponse>> Handle();
}