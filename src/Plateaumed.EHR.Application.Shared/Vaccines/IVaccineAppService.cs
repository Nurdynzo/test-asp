using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines;

public interface IVaccineAppService
{
    Task<List<GetAllVaccinesResponse>> GetAll();
    Task<GetVaccineResponse> GetVaccine(EntityDto<long> request);
    Task<List<GetAllVaccineGroupsResponse>> GetAllVaccineGroups();
    Task<GetVaccineGroupResponse> GetVaccineGroup(EntityDto<long> request);
}