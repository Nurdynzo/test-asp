using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using Newtonsoft.Json;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.ReviewAndSaves.Abstraction;

namespace Plateaumed.EHR.ReviewAndSaves.Handlers;
public class SaveToReviewDetailedHistoryCommandHandler : ISaveToReviewDetailedHistoryCommandHandler
{
    private readonly IRepository<PatientReviewDetailedHistory, long> _reviewDetailedHistoryRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IAbpSession _abpSession;
    private readonly IEncounterManager _encounterManager;
    private readonly IDoctorReviewAndSaveBaseQuery _patientEncounterQuery;



    public SaveToReviewDetailedHistoryCommandHandler(IRepository<PatientReviewDetailedHistory, long> reviewDetailedHistoryRepository,
            IUnitOfWorkManager unitOfWorkManager, 
            IAbpSession abpSession, 
            IEncounterManager encounterManager,
            IDoctorReviewAndSaveBaseQuery patientEncounterQuery)
    {
        _reviewDetailedHistoryRepository = reviewDetailedHistoryRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _abpSession = abpSession;
        _encounterManager = encounterManager;
        _patientEncounterQuery = patientEncounterQuery;
    }

    public async Task<ReviewDetailedHistoryReturnDto> Handle(SaveToReviewDetailedHistoryRequestDto requestDto)
    {
        var patientEncounter = await _patientEncounterQuery.GetPatientEncounter(requestDto.EncounterId, _abpSession.TenantId.GetValueOrDefault());
        var patientId = patientEncounter.PatientId;
        if (patientId <= 0)
            throw new UserFriendlyException("PatientId is required.");

        if (requestDto.EncounterId <= 0)
            throw new UserFriendlyException("Encounter Id is required.");

        var mappedModel = new PatientReviewDetailedHistory();
        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);
        var jsonData = JsonConvert.SerializeObject(requestDto.FirstVisitNote);
        var isExist = requestDto.Id == null ? false : _reviewDetailedHistoryRepository.GetAllList().Where(s=>s.Id == requestDto.Id).Any();
        if (isExist)
        {
            //if Id already exist, then get and update
            mappedModel = await _reviewDetailedHistoryRepository.GetAsync(requestDto.Id.GetValueOrDefault());
            mappedModel.PatientId = patientId;
            mappedModel.EncounterId = requestDto.EncounterId;
            mappedModel.Title = requestDto.Title;
            mappedModel.ShortDescription = requestDto.ShortDescription;
            mappedModel.Note = jsonData;
            mappedModel.IsAutoSaved = requestDto.IsAutoSaved ?? false;
            await _reviewDetailedHistoryRepository.UpdateAsync(mappedModel);
        }
        else
        {
            //If id does not exist, then save as new record.
            mappedModel = MapObject(requestDto, patientId, jsonData);

            await _reviewDetailedHistoryRepository.InsertAndGetIdAsync(mappedModel);
        }
        await _unitOfWorkManager.Current.SaveChangesAsync();
        return new ReviewDetailedHistoryReturnDto
        {
            Id = mappedModel.Id,
            CreationTime = mappedModel.CreationTime,
            Title = requestDto.Title,
            ShortDescription = requestDto.ShortDescription,
            FirstVisitNote = requestDto.FirstVisitNote,
            IsAutoSaved = requestDto.IsAutoSaved ?? false
        };
    }

    private PatientReviewDetailedHistory MapObject(SaveToReviewDetailedHistoryRequestDto requestDto, long patientId, string jsonData)
    {
        return new PatientReviewDetailedHistory
        {
            Id = requestDto.Id.GetValueOrDefault(),
            PatientId = patientId,
            EncounterId = requestDto.EncounterId,
            Title = requestDto.Title,
            ShortDescription = requestDto.ShortDescription,
            Note = jsonData,
            IsAutoSaved = requestDto.IsAutoSaved ?? false
        };
    }
}
