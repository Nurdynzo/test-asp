using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.VitalSigns.Dto;

namespace Plateaumed.EHR.VitalSigns.Handlers;

public class GetGcsScoringQueryHandler : IGetGcsScoringQueryHandler
{
    private readonly IRepository<Patient, long> _patientRepository; 
    private readonly IRepository<GCSScoring, long> _scoringRepository;
    private readonly IObjectMapper _objectMapper;

    public GetGcsScoringQueryHandler(
        IRepository<GCSScoring, long> scoringRepository,
        IRepository<Patient, long> patientRepository, 
        IObjectMapper objectMapper)
    {
        _patientRepository = patientRepository;
        _scoringRepository = scoringRepository;
        _objectMapper = objectMapper;
    }

    public async Task<List<GetGCSScoringResponse>> Handle(long patientId)
    {
        var patient = await _patientRepository.GetAsync(patientId) ?? throw new UserFriendlyException("Patient not found");
        var patientAge = DateTime.Now.Year - patient.DateOfBirth.Year;

        var gcsScorings = await _scoringRepository.GetAll().Include(x => x.Ranges)
            .Select(x => new GCSScoring
            {
                Id = x.Id,
                Name = x.Name,
                Ranges = x.Ranges.Where(r => r.AgeMin <= patientAge)
                    .Where(r => !r.AgeMax.HasValue || r.AgeMax > patientAge).ToList()
            }).ToListAsync();
        return _objectMapper.Map<List<GetGCSScoringResponse>>(gcsScorings);
    }
}