using System.Threading.Tasks;
using Abp.Dependency;
namespace Plateaumed.EHR.PriceSettings.Abstraction
{
    public interface IGetPriceSampleCsvFileForDownloadHandler : ITransientDependency
    {
        Task<byte[]> Handle();
    }
}
