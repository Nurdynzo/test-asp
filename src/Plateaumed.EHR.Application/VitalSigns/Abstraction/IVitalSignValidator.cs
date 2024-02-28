using System.Threading.Tasks;
using Abp.Dependency;

namespace Plateaumed.EHR.VitalSigns.Abstraction;

public interface IVitalSignValidator : ITransientDependency
{
    Task ValidateRequest(double vitalReading, VitalSign vitalSign, long? measurementRangeId);
}