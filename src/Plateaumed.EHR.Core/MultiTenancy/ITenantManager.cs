using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.MultiTenancy;

public interface ITenantManager : ITransientDependency
{
    Task<Tenant> GetByIdAsync(int id);
    Task UpdateAsync(Tenant tenant);
}