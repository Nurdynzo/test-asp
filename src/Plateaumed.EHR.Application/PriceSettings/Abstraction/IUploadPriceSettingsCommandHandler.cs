using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.PriceSettings.Dto;
namespace Plateaumed.EHR.PriceSettings.Abstraction
{
    public interface IUploadPriceSettingsCommandHandler : ITransientDependency
    {
        Task Handle(UpdatePricingCommandRequest request);
    }
}
