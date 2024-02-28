using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetPatientGynaecologicalIllnessSuggestionQueryHandler : IGetPatientGynaecologicalIllnessSuggestionQueryHandler
    {
        private readonly IRepository<PatientGynaecologicalIllnessSuggestion,long>
            _patientGynaecologicalIllnessSuggestionRepository;
        public GetPatientGynaecologicalIllnessSuggestionQueryHandler(IRepository<PatientGynaecologicalIllnessSuggestion, long> patientGynaecologicalIllnessSuggestionRepository)
        {
            _patientGynaecologicalIllnessSuggestionRepository = patientGynaecologicalIllnessSuggestionRepository;
        }
        
        public  Task<List<GetPatientGynaecologicalIllnessSuggestionQueryResponse>> Handle()
        {
            return  _patientGynaecologicalIllnessSuggestionRepository
                .GetAll()
                .Select(x => new GetPatientGynaecologicalIllnessSuggestionQueryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    SnomedId = x.SnomedId
                    
                })
                .ToListAsync();
        }
    }
}