using System.Threading.Tasks;
using Plateaumed.EHR.Admissions.Dto;

namespace Plateaumed.EHR.Admissions
{
    public class AdmissionsAppService : EHRAppServiceBase, IAdmissionsAppService
    {
        private readonly IAdmitPatientCommandHandler _admitPatient;
        private readonly ITransferPatientCommandHandler _transferPatientCommandHandler;
        private readonly ICompleteTransferPatientCommandHandler _completeTransferPatient;

        public AdmissionsAppService(IAdmitPatientCommandHandler admitPatient, 
            ITransferPatientCommandHandler transferPatientCommandHandler,
            ICompleteTransferPatientCommandHandler completeTransferPatient)
        {
            _admitPatient = admitPatient;
            _transferPatientCommandHandler = transferPatientCommandHandler;
            _completeTransferPatient = completeTransferPatient;
        }

        public async Task AdmitPatient(AdmitPatientRequest request)
        {
            await _admitPatient.Handle(request);
        }
        
        public async Task TransferPatient(TransferPatientRequest request)
        {
            await _transferPatientCommandHandler.Handle(request);
        }

        public async Task CompleteTransferPatient(long encounterId)
        {
            await _completeTransferPatient.Handle(encounterId);
        }
    }
}
