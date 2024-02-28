using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Plateaumed.EHR.Procedures.Abstractions;
namespace Plateaumed.EHR.Procedures.Handlers
{
    public class DeleteSpecializedProcedureNurseDetailQueryHandler : IDeleteSpecializedProcedureNurseDetailQueryHandler
    {
        private readonly IRepository<SpecializedProcedureNurseDetail,long> _specializedProcedureNurseDetailRepository;
        public DeleteSpecializedProcedureNurseDetailQueryHandler(IRepository<SpecializedProcedureNurseDetail, long> specializedProcedureNurseDetailRepository)
        {
            _specializedProcedureNurseDetailRepository = specializedProcedureNurseDetailRepository;
        }
        public async Task Handle(long id)
        {
            var nurseDetail = await _specializedProcedureNurseDetailRepository.GetAsync(id)
                ?? throw new UserFriendlyException("Nurse detail not found.");
            await _specializedProcedureNurseDetailRepository.DeleteAsync(nurseDetail);
        }
    }
}
