using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.PatientProfile.Abstraction;

public interface IDeletePatientMajorInjuryCommandHandler: ITransientDependency
{
    Task Handle(long id);
}