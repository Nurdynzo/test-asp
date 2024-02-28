using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.NurseHistory.Dtos;

namespace Plateaumed.EHR.NurseHistory.Abstractions;

public interface IGetNurseHistoryQueryHandler : ITransientDependency
{
    Task<List<NurseHistoryResponseDto>> Handle(long patientId);
}