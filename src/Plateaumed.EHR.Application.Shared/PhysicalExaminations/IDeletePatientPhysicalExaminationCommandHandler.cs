using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.PhysicalExaminations;

public interface IDeletePatientPhysicalExaminationCommandHandler : ITransientDependency
{
    Task Handle(long patientPhysicalExaminationId);
}