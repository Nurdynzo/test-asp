using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines.Abstractions;

public interface ICreateVaccinationHistoryCommandHandler : ITransientDependency
{
    Task Handle(CreateMultipleVaccinationHistoryDto requestListDto);
}