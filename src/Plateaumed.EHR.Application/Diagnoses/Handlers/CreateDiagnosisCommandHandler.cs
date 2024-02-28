using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Plateaumed.EHR.Diagnoses.Abstraction;

namespace Plateaumed.EHR.Diagnoses.Handlers
{
    public class CreateDiagnosisCommandHandler : ICreateDiagnosisCommandHandler
    {
        private readonly IRepository<Diagnosis, long> _diagnosisRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CreateDiagnosisCommandHandler(IRepository<Diagnosis, long> diagnosisRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _diagnosisRepository = diagnosisRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<Diagnosis> Handle(Diagnosis diagnosis)
        {
            await _diagnosisRepository.InsertAsync(diagnosis);
            await _unitOfWorkManager.Current.SaveChangesAsync();

            return diagnosis;
        }
    }
}
