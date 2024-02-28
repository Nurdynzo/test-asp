using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface IEditPatientAllergyCommandHandler : ITransientDependency
    {
        Task Handle(EditPatientAllergyCommandRequest request);
    }
}
