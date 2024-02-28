using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Procedures.Dtos;
namespace Plateaumed.EHR.Procedures.Abstractions
{
    public interface IGetSpecializedProcedureSafetyCheckListQueryHandler:ITransientDependency
    {
        Task<SpecializedProcedureSafetyCheckListDto> Handle(long patientId, long procedureId);
    }
}
