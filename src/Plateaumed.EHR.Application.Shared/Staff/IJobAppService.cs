using System.Threading.Tasks;
using Abp.Application.Services;
using Plateaumed.EHR.Staff.Dtos;

namespace Plateaumed.EHR.Staff;

public interface IJobAppService : IApplicationService
{
    Task Create(CreateOrEditJobRequest request);
    Task Update(CreateOrEditJobRequest request);
}