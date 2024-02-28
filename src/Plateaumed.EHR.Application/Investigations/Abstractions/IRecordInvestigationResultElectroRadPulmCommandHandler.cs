using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Investigations.Dto;

namespace Plateaumed.EHR.Investigations.Abstractions
{
    public interface IRecordInvestigationResultElectroRadPulmCommandHandler: ITransientDependency
    {
        Task Handle(ElectroRadPulmInvestigationResultRequestDto request, long facilityId, bool skipUploadService = false);
    }
}

