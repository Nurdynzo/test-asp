using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines.Abstractions;

public interface ICreateVaccinationCommandHandler : ITransientDependency
{
    Task Handle(CreateMultipleVaccinationDto requestListDto);
}