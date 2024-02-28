using System;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Plateaumed.EHR.Feeding.Abstractions;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Feeding.Query.BaseQueryHelper;

public class FeedingBaseQuery : IBaseQuery
{
    private readonly IRepository<AllInputs.Feeding, long> _feedingRepository;
    private readonly IRepository<AllInputs.FeedingSuggestions, long> _feedingSuggestionsRepository;

    public FeedingBaseQuery(IRepository<AllInputs.Feeding, long> feedingRepository, IRepository<AllInputs.FeedingSuggestions, long> feedingSuggestionsRepository )
    {
        _feedingRepository = feedingRepository;
        _feedingSuggestionsRepository = feedingSuggestionsRepository;
    }

    public IQueryable<AllInputs.Feeding> GetPatientFeedingBaseQuery(int patientId)
    {
        return _feedingRepository.GetAll()
            .Where(a => a.PatientId == patientId);
    }

    public IQueryable<AllInputs.FeedingSuggestions> GetPatientFeedingSuggestionsBaseQuery()
    {
        return _feedingSuggestionsRepository.GetAll();
    }
}