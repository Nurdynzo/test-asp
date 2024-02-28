using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations.Handlers;

public class GetPatientPhysicalExamSummaryWithEncounterQueryHandler : IGetPatientPhysicalExamSummaryWithEncounterQueryHandler
{
    private readonly IRepository<PatientPhysicalExamination, long> _repository;
    private readonly IRepository<PatientPhysicalExaminationImageFile, long> _patientPhysicalExamImageFileRepository;
    private readonly IObjectMapper _objectMapper;

    public GetPatientPhysicalExamSummaryWithEncounterQueryHandler(IRepository<PatientPhysicalExamination, long> repository, IObjectMapper objectMapper, IRepository<PatientPhysicalExaminationImageFile, long> patientPhysicalExamImageFileRepository)
    {
        _repository = repository;
        _objectMapper = objectMapper;
        _patientPhysicalExamImageFileRepository = patientPhysicalExamImageFileRepository;
    }

    public async Task<List<PatientPhysicalExaminationResponseDto>> Handle(long patientId, long encounterId, int? tenantId)
    {
        var patientPhysicalExaminations = await _repository.GetAll()
            .Include(v => v.TypeNotes)
            .Include(v => v.PhysicalExaminationType)
            .Include(v => v.Suggestions)
                .ThenInclude(x => x.PatientPhysicalExamSuggestionAnswers)
                    .ThenInclude(x => x.Sites)

            .Include(v => v.Suggestions)
                .ThenInclude(x => x.PatientPhysicalExamSuggestionAnswers)
                    .ThenInclude(x => x.Planes)

            .Include(v => v.Suggestions)
                .ThenInclude(x => x.PatientPhysicalExamSuggestionAnswers)
                    .ThenInclude(x => x.Qualifiers)

            .Where(x => x.PatientId == patientId && x.EncounterId == encounterId && x.TenantId == tenantId && x.IsDeleted == false)
            .OrderByDescending(v => v.CreationTime)
            .Select(v => _objectMapper.Map<PatientPhysicalExaminationResponseDto>(v))
            .ToListAsync();

        foreach (var patientPhysicalExamination in patientPhysicalExaminations)
            patientPhysicalExamination.ImageUploaded = await _patientPhysicalExamImageFileRepository.GetAll().AnyAsync(
                v => v.PatientPhysicalExaminationId == patientPhysicalExamination.Id);

        // return response
        return patientPhysicalExaminations;
    }
}