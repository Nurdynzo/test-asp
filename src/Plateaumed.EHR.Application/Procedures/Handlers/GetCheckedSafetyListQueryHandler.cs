using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;
namespace Plateaumed.EHR.Procedures.Handlers
{
    public class GetCheckedSafetyListQueryHandler : IGetCheckedSafetyListQueryHandler
    {
        private readonly IRepository<SpecializedProcedureSafetyCheckList, long> _specializedProcedureSafetyCheckListRepository;
        public GetCheckedSafetyListQueryHandler(IRepository<SpecializedProcedureSafetyCheckList, long> specializedProcedureSafetyCheckListRepository)
        {
            _specializedProcedureSafetyCheckListRepository = specializedProcedureSafetyCheckListRepository;
        }
        public async Task<SpecializedProcedureSafetyCheckListDto> Handle(long patientId, long procedureId)
        {
            var query = await _specializedProcedureSafetyCheckListRepository
                .FirstOrDefaultAsync(x => x.ProcedureId == procedureId && x.PatientId == patientId)
                ?? throw new UserFriendlyException("SafetyCheckList not found for the given patient and procedure");
            return new SpecializedProcedureSafetyCheckListDto
            {
                PatientId = query.PatientId,
                ProcedureId = query.ProcedureId,
                CheckLists = query.CheckLists.Where(x => x.Checked).ToList()
            };
        }
    }
}
