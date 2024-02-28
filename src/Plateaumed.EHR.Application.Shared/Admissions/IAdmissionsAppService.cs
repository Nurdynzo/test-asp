using System.Threading.Tasks;
using Plateaumed.EHR.Admissions.Dto;

namespace Plateaumed.EHR.Admissions;

public interface IAdmissionsAppService
{
    Task AdmitPatient(AdmitPatientRequest request);
    Task TransferPatient(TransferPatientRequest request);
    Task CompleteTransferPatient(long encounterId);
}