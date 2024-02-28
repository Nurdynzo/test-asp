using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.ReviewAndSaves.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Abstraction;

public interface ICreateNursingRecordCommandHandler : ITransientDependency
{
    Task<NursingRecordDto> Handle(NursingRecordDto requestDto);
}