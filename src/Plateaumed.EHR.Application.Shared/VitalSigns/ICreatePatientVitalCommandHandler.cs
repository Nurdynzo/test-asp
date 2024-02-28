using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Runtime.Session;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.VitalSigns;

public interface ICreatePatientVitalCommandHandler : ITransientDependency
{
    Task Handle(CreateMultiplePatientVitalDto requestDto);
}