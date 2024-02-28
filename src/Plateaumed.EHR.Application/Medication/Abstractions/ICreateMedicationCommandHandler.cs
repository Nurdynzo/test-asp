using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Medication.Dtos;

namespace Plateaumed.EHR.Medication.Abstractions;

public interface ICreateMedicationCommandHandler : ITransientDependency
{
    Task Handle(CreateMultipleMedicationsDto requestDto);
}