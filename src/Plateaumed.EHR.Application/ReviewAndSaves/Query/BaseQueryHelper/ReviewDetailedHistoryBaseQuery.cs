using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;

namespace Plateaumed.EHR.ReviewAndSaves.Query.BaseQueryHelper;

public class ReviewDetailedHistoryBaseQuery : IReviewDetailedHistoryBaseQuery
{
    private readonly IRepository<PatientReviewDetailedHistory, long> _reviewDetailedHistoryRepository;


    public ReviewDetailedHistoryBaseQuery(IRepository<PatientReviewDetailedHistory, long> reviewDetailedHistoryRepository)
    {
        _reviewDetailedHistoryRepository = reviewDetailedHistoryRepository;
    }

    public async Task<PatientReviewDetailedHistory> GetById(long input)
    {
        var response = await _reviewDetailedHistoryRepository.GetAsync(input);

        return response;
    }
    
}
