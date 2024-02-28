using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;

namespace Plateaumed.EHR.PhysicalExaminations.Handlers;

public class DeletePatientPhysicalExamImageQueryHandler : IDeletePatientPhysicalExamImageQueryHandler
{
    private readonly IRepository<PatientPhysicalExaminationImageFile, long> _repository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public DeletePatientPhysicalExamImageQueryHandler(IRepository<PatientPhysicalExaminationImageFile, long> repository, IUnitOfWorkManager unitOfWorkManager)
    {
        _repository = repository;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task Handle(long patientPhysicalExaminationImageFileId)
    {
        // get image file
        var imageFile = await _repository.GetAsync(patientPhysicalExaminationImageFileId) ??
                        throw new UserFriendlyException(
                            $"No patient physical examination image found.");
        
        // persist
        await _repository.DeleteAsync(imageFile);
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }
}