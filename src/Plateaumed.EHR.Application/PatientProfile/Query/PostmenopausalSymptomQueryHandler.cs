using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
namespace Plateaumed.EHR.PatientProfile.Query
{
    public class PostmenopausalSymptomQueryHandler : IPostmenopausalSymptomQueryHandler
    {
        private readonly IRepository<PostmenopausalSymptomSuggestion, long> _postmenopausalSymptomSuggestionRepository;
        public PostmenopausalSymptomQueryHandler(
            IRepository<PostmenopausalSymptomSuggestion, long> postmenopausalSymptomSuggestionRepository)
        {
            _postmenopausalSymptomSuggestionRepository = postmenopausalSymptomSuggestionRepository;
        }

        public  Task<List<PostmenopausalSymptomSuggestionResponse>> Handle()
        {
            return _postmenopausalSymptomSuggestionRepository
                .GetAll()
                .Select(x => new PostmenopausalSymptomSuggestionResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    SnomedId = x.SnomedId

                }).ToListAsync();
        }
    }
}