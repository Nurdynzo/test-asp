using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Plateaumed.EHR.PatientProfile.Dto;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetPatientGynaecologicProcedureSuggestionQueryHandler : IGetPatientGynaecologicProcedureSuggestionQueryHandler
    {
        private readonly IRepository<PatientGynaecologicProcedureSuggestion, long> _patientGynaecologicProcedureSuggestionRepository;
        public GetPatientGynaecologicProcedureSuggestionQueryHandler(IRepository<PatientGynaecologicProcedureSuggestion, long> patientGynaecologicProcedureSuggestionRepository)
        {
            _patientGynaecologicProcedureSuggestionRepository = patientGynaecologicProcedureSuggestionRepository;
        }

        public Task<List<PatientGynaecologicProcedureSuggestionResponse>> Handle()
        {
            return _patientGynaecologicProcedureSuggestionRepository
                .GetAll()
                .Select(x =>
                new PatientGynaecologicProcedureSuggestionResponse()
                {
                    Name = x.Name,
                    SnomedId = x.SnomedId
                }).ToListAsync();
        }
    }
}