using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.PhysicalExaminations.Dto;

namespace Plateaumed.EHR.PhysicalExaminations.Handlers;

public class GetPatientPhysicalExaminationSummaryQueryHandler : IGetPatientPhysicalExaminationSummaryQueryHandler
{
    private readonly IRepository<PatientPhysicalExamination, long> _repository;
    private readonly IRepository<PatientPhysicalExaminationImageFile, long> _patientPhysicalExamImageFileRepository;
    private readonly IRepository<User,long> _userRepository;
    private readonly IAbpSession _abpSession;
    private readonly IObjectMapper _objectMapper;

    public GetPatientPhysicalExaminationSummaryQueryHandler(
        IRepository<PatientPhysicalExamination, long> repository,
        IObjectMapper objectMapper,
        IRepository<PatientPhysicalExaminationImageFile, long> patientPhysicalExamImageFileRepository,
        IAbpSession abpSession, IRepository<User, long> userRepository)
    {
        _repository = repository;
        _objectMapper = objectMapper;
        _patientPhysicalExamImageFileRepository = patientPhysicalExamImageFileRepository;
        _abpSession = abpSession;
        _userRepository = userRepository;
    }

    public async Task<List<PatientPhysicalExaminationResponseDto>> Handle(long patientId, long? procedureId = null)
    {
        var patientPhysicalExaminations = await (from p in _repository.GetAll()
                                                     .IgnoreQueryFilters()
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
                                                 join u in _userRepository.GetAll() on p.DeleterUserId equals u.Id into user
                                                 from u in user.DefaultIfEmpty()
                                                 where p.PatientId == patientId && p.TenantId == _abpSession.TenantId && (procedureId == null || p.ProcedureId == procedureId)
                                                 orderby p.CreationTime descending
                                                 select new PatientPhysicalExaminationResponseDto
                                                 {
                                                     Id = p.Id,
                                                     PhysicalExaminationEntryType = p.PhysicalExaminationEntryType,
                                                     PhysicalExaminationTypeId = p.PhysicalExaminationTypeId,
                                                     PhysicalExaminationType = _objectMapper.Map<GetPhysicalExaminationTypeResponseDto>(p.PhysicalExaminationType),
                                                     PatientId = p.PatientId,
                                                     OtherNote = p.OtherNote,
                                                     CreationTime = p.CreationTime,
                                                     DeletionTime = p.DeletionTime,
                                                     TypeNotes = _objectMapper.Map<List<PatientPhysicalExamTypeNoteRequestDto>>(p.TypeNotes),
                                                     Suggestions = _objectMapper.Map<List<PatientPhysicalExamSuggestionQuestionDto>>(p.Suggestions),
                                                     ProcedureEntryType = p.ProcedureEntryType != null ? p.ProcedureEntryType.ToString() : null,
                                                     ImageUploaded = _patientPhysicalExamImageFileRepository
                                                         .GetAll()
                                                         .Any(x => x.PatientPhysicalExaminationId == p.Id),
                                                     EncounterId = p.EncounterId,
                                                     ProcedureId = p.ProcedureId,
                                                     PhysicalExaminationEntryTypeName = p.PhysicalExaminationEntryType.ToString(),
                                                     IsDeleted = p.IsDeleted,
                                                     DeletedUser = u == null ? string.Empty : u.DisplayName
                                                 }
            )
            .ToListAsync();
        // return response
        return patientPhysicalExaminations;
    }
}
