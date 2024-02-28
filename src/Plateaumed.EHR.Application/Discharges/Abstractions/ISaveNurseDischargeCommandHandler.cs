using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Discharges.Dtos;

namespace Plateaumed.EHR.Discharges.Abstractions;

public interface ISaveNurseDischargeCommandHandler : ITransientDependency
{
    Task<DischargeDto> Handle(FinalizeNurseDischargeDto requestDto);
}
