using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;

namespace Plateaumed.EHR.PhysicalExaminations.Handlers;

public class DeletePatientPhysicalExaminationCommandHandler : IDeletePatientPhysicalExaminationCommandHandler
{
    private readonly IRepository<PatientPhysicalExamination, long> _repository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public DeletePatientPhysicalExaminationCommandHandler(IRepository<PatientPhysicalExamination, long> repository, IUnitOfWorkManager unitOfWorkManager)
    {
        _repository = repository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Handle(long patientPhysicalExaminationId)
    {
        // get patientPhysicalExamination item
        var patientPhysicalExamination = await _repository.GetAsync(patientPhysicalExaminationId) ??
                                         throw new UserFriendlyException(
                                             $"patient physical examination with Id {patientPhysicalExaminationId} does not exist.");
        
        // persist
        await _repository.DeleteAsync(patientPhysicalExamination);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }
}