using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;
namespace Plateaumed.EHR.Procedures.Handlers
{
    public class UpdateSpecializedProcedureCheckListCommandHandler : IUpdateSpecializedProcedureCheckListCommandHandler
    {
        private readonly IRepository<SpecializedProcedureSafetyCheckList, long> _specializedProcedureSafetyCheckListRepository;
        private readonly IUnitOfWorkManager _unitOfWork;
        public UpdateSpecializedProcedureCheckListCommandHandler(
            IRepository<SpecializedProcedureSafetyCheckList, long> specializedProcedureSafetyCheckListRepository,
            IUnitOfWorkManager unitOfWork)
        {
            _specializedProcedureSafetyCheckListRepository = specializedProcedureSafetyCheckListRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(SpecializedProcedureSafetyCheckListDto request)
        {
            if (request.CheckLists.Count == 0)
            {
                throw new UserFriendlyException("CheckLists cannot be empty.");
            }
            request.CheckLists.ForEach(x =>
            {
                if(x.Checked) x.DateChecked = DateTime.UtcNow;
            });

            var query = await _specializedProcedureSafetyCheckListRepository
                            .FirstOrDefaultAsync(x => x.ProcedureId == request.ProcedureId && x.PatientId == request.PatientId)
                        ?? throw new UserFriendlyException("SafetyCheckList not found for the given patient and procedure");
            query.CheckLists = request.CheckLists;
            await _specializedProcedureSafetyCheckListRepository.UpdateAsync(query);
            await _unitOfWork.Current.SaveChangesAsync();
        }
    }
}
