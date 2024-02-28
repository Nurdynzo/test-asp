using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PatientProfile.Dto;
namespace Plateaumed.EHR.PatientProfile.Abstraction
{
    public interface ISaveMenstruationAndFrequencyCommandHandler: ITransientDependency
    {
        Task<SaveMenstruationAndFrequencyCommand> Handle(SaveMenstruationAndFrequencyCommand request);
    }
}