using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;

namespace Plateaumed.EHR.PatientProfile.Abstraction;

public interface ICreatePatientAllergyCommandHandler : ITransientDependency
{
    Task Handle(CreatePatientAllergyCommandRequest request);
}