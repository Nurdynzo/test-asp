using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Admissions.Dto;

namespace Plateaumed.EHR.Admissions;

public interface ITransferPatientCommandHandler : ITransientDependency
{
    Task Handle(TransferPatientRequest request);
}