using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Plateaumed.EHR.PhysicalExaminations;
using Plateaumed.EHR.PhysicalExaminations.Abstraction;
using Plateaumed.EHR.PhysicalExaminations.Dto;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;
using Plateaumed.EHR.ReviewAndSaves.Dtos;
using Plateaumed.EHR.Symptom.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Query;

public class GetReviewAndSavePatientTypeNotesQueryHandler : IGetReviewAndSavePatientTypeNotesQueryHandler
{
    private readonly IGetPhysicalExaminationUploadedImagesQueryHandler _repositoryPhysicalExaminaionImageQuery;
    private readonly IAbpSession _abpSession;



    public GetReviewAndSavePatientTypeNotesQueryHandler(
        IGetPhysicalExaminationUploadedImagesQueryHandler repositoryPhysicalExaminaionImageQuery,
        IAbpSession abpSession)
    {
        _repositoryPhysicalExaminaionImageQuery = repositoryPhysicalExaminaionImageQuery;
        _abpSession = abpSession;
    }

    public async Task<List<PatientTypeNoteDto>> Handle(long doctorUserId, List<PatientSymptomSummaryForReturnDto> symptoms,
                                                            List<PatientPhysicalExaminationResponseDto> physicalExamination)
    {
        var typedNote = new List<PatientTypeNoteDto>();
        //Filter symptoms when entry type is typenote
        var presentingComplaintsTypeNote = symptoms == null ? null : symptoms.Where(x => (x.CreatorUserId == _abpSession.UserId.GetValueOrDefault() ||
                                                                    x.CreatorUserId == doctorUserId) &&
                                                                    x.SymptomEntryType == Symptom.SymptomEntryType.TypeNote)
                                                                    .Select(s => s.TypeNotes).ToList();

        foreach (var item in presentingComplaintsTypeNote)
        {
            typedNote.AddRange(item.Select(s => new PatientTypeNoteDto()
            {
                Type = s.Type,
                Note = s.Note
            }).ToList());
        }

        //Filter Physical Examination Type notes
        var physicalExaminationTypeNote = physicalExamination.Where(t => t.PhysicalExaminationEntryType == PhysicalExaminationEntryType.TypeNote)
                                                                    .ToList();
        foreach (var item in physicalExaminationTypeNote)
        {
            var tnote = item.TypeNotes;
            var patientPyhicalExamImages = item.ImageUploaded ? await _repositoryPhysicalExaminaionImageQuery.Handle(item.Id) : null;
            typedNote.AddRange(tnote.Select(s => new PatientTypeNoteDto()
            {
                Type = s.Type,
                Note = s.Note,
                ImageFiles = patientPyhicalExamImages,
                ImageUploaded = item.ImageUploaded
            }).ToList());
        }

        return typedNote;
    }
}
