using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Patients;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;
namespace Plateaumed.EHR.Procedures.Handlers
{
    public class GetSpecializedProcedureSafetyCheckListQueryHandler : IGetSpecializedProcedureSafetyCheckListQueryHandler
    {
        private readonly IRepository<SpecializedProcedureSafetyCheckList, long> _specializedProcedureSafetyCheckListRepository;
        private readonly IRepository<Patient,long> _patientRepository;
        private readonly IRepository<Procedure,long> _procedureRepository;
        private readonly IUnitOfWorkManager _unitOfWork;
        public GetSpecializedProcedureSafetyCheckListQueryHandler(
            IRepository<SpecializedProcedureSafetyCheckList, long> specializedProcedureSafetyCheckListRepository,
            IRepository<Patient, long> patientRepository,
            IRepository<Procedure, long> procedureRepository,
            IUnitOfWorkManager unitOfWork)
        {
            _specializedProcedureSafetyCheckListRepository = specializedProcedureSafetyCheckListRepository;
            _patientRepository = patientRepository;
            _procedureRepository = procedureRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SpecializedProcedureSafetyCheckListDto> Handle(long patientId, long procedureId)
        {
            await ValidatePatientAndProcedureExist(patientId, procedureId);
            var query = await GetCheckListAsync(patientId, procedureId);
            if (query != null)
                return query;
            var specializedProcedureSafetyCheckList = new SpecializedProcedureSafetyCheckList
            {
                PatientId = patientId,
                ProcedureId = procedureId,
                CheckLists = SafetyCheckList.GetDefaultList()
            };
            await _specializedProcedureSafetyCheckListRepository.InsertAsync(specializedProcedureSafetyCheckList);
            await _unitOfWork.Current.SaveChangesAsync();
            return await GetCheckListAsync(patientId, procedureId);
        }
        private Task<SpecializedProcedureSafetyCheckListDto> GetCheckListAsync(long patientId, long procedureId)
        {

            return _specializedProcedureSafetyCheckListRepository
                .GetAll()
                .Select(x => new SpecializedProcedureSafetyCheckListDto
                {
                    PatientId = x.PatientId,
                    ProcedureId = x.ProcedureId,
                    CheckLists = x.CheckLists
                })
                .FirstOrDefaultAsync(x =>
                    x.PatientId == patientId &&
                    x.ProcedureId == procedureId);
        }
        private async Task ValidatePatientAndProcedureExist(long patientId, long procedureId)
        {
           _= await _patientRepository.GetAsync(patientId) ?? throw new UserFriendlyException($"Patient with Id {patientId} does not exist");
           _= await _procedureRepository.GetAsync(procedureId) ?? throw new UserFriendlyException($"Procedure with Id {procedureId} does not exist");
        }

    }
}
