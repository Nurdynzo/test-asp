using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.PhysicalExaminations;

public interface IDeletePatientPhysicalExamImageQueryHandler : ITransientDependency
{
    Task Handle(long patientPhysicalExaminationImageFileId);
}