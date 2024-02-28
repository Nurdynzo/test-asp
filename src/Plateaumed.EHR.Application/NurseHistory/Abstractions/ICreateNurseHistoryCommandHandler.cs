using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.NurseHistory.Dtos;

namespace Plateaumed.EHR.NurseHistory.Abstractions;

public interface ICreateNurseHistoryCommandHandler : ITransientDependency
{
    Task Handle(CreateNurseHistoryDto requestDto);
}