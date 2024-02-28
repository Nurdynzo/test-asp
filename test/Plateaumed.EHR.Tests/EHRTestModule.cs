using Abp.Modules;
using Plateaumed.EHR.Test.Base;

namespace Plateaumed.EHR.Tests
{
    [DependsOn(typeof(EHRTestBaseModule))]
    public class EHRTestModule : AbpModule
    {
       
    }
}
