using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Investigations.Abstractions;
using Plateaumed.EHR.Investigations.Dto;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Investigations.Handlers
{
    public class GetInvestigationQueryHandler : IGetInvestigationQueryHandler
    {
        private readonly IRepository<Investigation, long> _repository;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<InvestigationSuggestion, long> _suggestionRepository;
        private readonly IRepository<Patient, long> _patientRepository;

        public GetInvestigationQueryHandler(IRepository<Investigation, long> repository,
            IObjectMapper objectMapper, IRepository<InvestigationSuggestion, long> suggestionRepository,
            IRepository<Patient, long> patientRepository)
        {
            _repository = repository;
            _objectMapper = objectMapper;
            _suggestionRepository = suggestionRepository;
            _patientRepository = patientRepository;
        }

        public async Task<GetInvestigationResponse> Handle(GetInvestigationRequest request)
        {
            var investigation = await BaseQuery(request.Id).FirstOrDefaultAsync() ??
                                throw new UserFriendlyException("Investigation not found");

            var patient = await _patientRepository.GetAsync(request.PatientId) ?? 
                          throw new UserFriendlyException("Patient not found");

            return MapInvestigation(investigation, patient);
        }

        private GetInvestigationResponse MapInvestigation(Investigation investigation, Patient patient)
        {
            var response = _objectMapper.Map<GetInvestigationResponse>(investigation);

            if (investigation.Type == InvestigationTypes.Microbiology)
            {
                response.Suggestions.AddRange(
                    _objectMapper.Map<List<InvestigationSuggestionDto>>(GetMicrobiologySuggestions(investigation)));
                response.NugentScore = investigation.Microbiology.NugentScore;
            }

            if (response.Ranges.Any())
            {
                response.Ranges = response.Ranges
                    .Where(x => !x.Gender.HasValue || patient.GenderType == x.Gender)
                    .Where(x => !x.AgeMax.HasValue || patient.DateOfBirth >= CalculateAge(x.AgeMax.Value, x.AgeMaxUnit))
                    .Where(x => !x.AgeMin.HasValue || patient.DateOfBirth < CalculateAge(x.AgeMin.Value, x.AgeMinUnit))
                    .ToList();
            }

            if (investigation.Components.Any())
            {
                var nextLevel = investigation.Components
                    .Select(x => MapInvestigation(BaseQuery(x.Id).FirstOrDefault(), patient))
                    .ToList();
                response.Components = nextLevel;
            }

            return response;
        }

        private DateTime CalculateAge(int age, UnitOfTime? unit)
        {
            return unit switch
            {
                UnitOfTime.Day => DateTime.Now.AddDays(-age),
                UnitOfTime.Week => DateTime.Now.AddDays(-age * 7),
                UnitOfTime.Month => DateTime.Now.AddMonths(-age),
                UnitOfTime.Year => DateTime.Now.AddYears(-age),
                _ => throw new ArgumentOutOfRangeException(nameof(unit), unit, null)
            };
        }
        
        private IQueryable<Investigation> BaseQuery(long id)
        {
            return _repository.GetAll()
                .Include(x => x.Suggestions)
                .Include(x => x.Ranges)
                .Include(x => x.Results)
                .Include(x => x.Microbiology)
                .Include(x => x.Dipstick)
                .ThenInclude(x => x.Ranges)
                .ThenInclude(x => x.Results)
                .Include(x => x.Components)
                .Where(x => x.Id == id);
        }

        private List<InvestigationSuggestion> GetMicrobiologySuggestions(Investigation investigation)
        {
            var requiredCategories = new Dictionary<string, bool>
            {
                { SuggestionCategories.BlueStain, investigation.Microbiology.MethyleneBlueStain },
                { SuggestionCategories.GramStain, investigation.Microbiology.GramStain },
                { SuggestionCategories.Microscopy, investigation.Microbiology.Microscopy },
                { SuggestionCategories.AntibioticSensitivity, investigation.Microbiology.AntibioticSensitivityTest },
                { SuggestionCategories.Culture, investigation.Microbiology.Culture },
                { SuggestionCategories.CommonMicrobiology, investigation.Microbiology.CommonResults },
            }.Where(i => i.Value).Select(i => i.Key);

            return  _suggestionRepository.GetAll().Where(x => requiredCategories.Contains(x.Category)).ToList();
        }
    }
}