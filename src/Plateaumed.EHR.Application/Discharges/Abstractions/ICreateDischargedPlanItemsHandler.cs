using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Discharges.Dtos;

namespace Plateaumed.EHR.Discharges.Abstractions;

public interface ICreateDischargedPlanItemsHandler : ITransientDependency
{
    Task<List<DischargePlanItemDto>> Handle(List<CreateDischargePlanItemDto> requestDto, long dischargeId, long patientId);
}