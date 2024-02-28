using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Plateaumed.EHR.Medication.Abstractions;
using Plateaumed.EHR.Medication.Handlers.Common;
namespace Plateaumed.EHR.Medication.Handlers
{
    public class MarkMedicationForDiscontinueCommandHandler : IMarkMedicationForDiscontinueCommandHandler
    {
        private readonly IRepository<AllInputs.Medication,long> _medicationRepository;
        private readonly IAbpSession _abpSession;
        public MarkMedicationForDiscontinueCommandHandler(IRepository<AllInputs.Medication, long> medicationRepository, IAbpSession abpSession)
        {
            _medicationRepository = medicationRepository;
            _abpSession = abpSession;
        }
        public async Task Handle(List<long> medicationId)
        {
            await CommonMedicationUpdate
                .UpdateIsAdministerOrIsContinue(
                    _medicationRepository,
                    medicationId,
                    isDiscontinue: true,discontinueUserId:_abpSession.UserId);
        }
    }
}
