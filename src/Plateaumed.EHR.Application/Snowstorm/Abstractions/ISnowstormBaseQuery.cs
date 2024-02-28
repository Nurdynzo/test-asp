using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Snowstorm.Dtos;

namespace Plateaumed.EHR.Snowstorm.Abstractions;

public interface ISnowstormBaseQuery : ITransientDependency
{
    Task<(SnowstormResponse snowstormResponse, bool status)> GetTermBySemanticTags(SnowstormRequestDto request);
}