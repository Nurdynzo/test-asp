using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Procedures.Dtos;
namespace Plateaumed.EHR.Procedures.Abstractions
{
    public interface IGetCheckedSafetyListQueryHandler : ITransientDependency
    {
        Task<SpecializedProcedureSafetyCheckListDto> Handle(long patientId, long procedureId);
    }
}
