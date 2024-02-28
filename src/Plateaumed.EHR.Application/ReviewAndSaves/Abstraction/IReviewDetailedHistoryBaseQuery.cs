using System.Threading.Tasks;
using Abp.Dependency;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.ReviewAndSaves.Abstraction
{
    public interface IReviewDetailedHistoryBaseQuery : ITransientDependency
    {
        Task<PatientReviewDetailedHistory> GetById(long input);
    }
}
