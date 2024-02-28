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
using Plateaumed.EHR.ReviewAndSaves.Dtos;

namespace Plateaumed.EHR.ReviewAndSaves.Handlers;
public class CreateNursingRecordCommandHandler : ICreateNursingRecordCommandHandler
{
    private readonly IRepository<NursingRecord, long> _nursingRecordRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IAbpSession _abpSession;
    private readonly IEncounterManager _encounterManager;
    private readonly IDoctorReviewAndSaveBaseQuery _patientEncounterQuery;



    public CreateNursingRecordCommandHandler(IRepository<NursingRecord, long> nursingRecordRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IAbpSession abpSession,
            IEncounterManager encounterManager,
            IDoctorReviewAndSaveBaseQuery patientEncounterQuery)
    {
        _nursingRecordRepository = nursingRecordRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _abpSession = abpSession;
        _encounterManager = encounterManager;
        _patientEncounterQuery = patientEncounterQuery;
    }

    public async Task<NursingRecordDto> Handle(NursingRecordDto requestDto)
    {
        if (requestDto.EncounterId <= 0)
            throw new UserFriendlyException("Encounter Id is required.");

        var patientEncounter = await _patientEncounterQuery.GetPatientEncounter(requestDto.EncounterId, _abpSession.TenantId.GetValueOrDefault());
        var patientId = patientEncounter.PatientId;


        var mappedModel = new NursingRecord();
        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);
        var jsonData = JsonConvert.SerializeObject(requestDto);
        var isExist = requestDto.Id == null ? false : _nursingRecordRepository.GetAllList().Where(s => s.Id == requestDto.Id).Any();
        if (isExist)
        {
            //if Id already exist, then get and update
            mappedModel = await _nursingRecordRepository.GetAsync(requestDto.Id.GetValueOrDefault());
            mappedModel.EncounterId = requestDto.EncounterId;
            mappedModel.JsonNote = jsonData;
            mappedModel.IsAutoSaved = requestDto.IsAutoSaved ?? false;
            await _nursingRecordRepository.UpdateAsync(mappedModel);
        }
        else
        {
            //If id does not exist, then save as new record.
            mappedModel = MapObject(requestDto, jsonData);

            await _nursingRecordRepository.InsertAndGetIdAsync(mappedModel);
        }
        await _unitOfWorkManager.Current.SaveChangesAsync();
        return new NursingRecordDto
        {
            Id = mappedModel.Id,
            CreationTime = mappedModel.CreationTime,
            NursingNote = requestDto.NursingNote,
            IsAutoSaved = requestDto.IsAutoSaved ?? false
        };
    }

    private NursingRecord MapObject(NursingRecordDto requestDto, string jsonData)
    {
        return new NursingRecord
        {
            Id = requestDto.Id.GetValueOrDefault(),
            EncounterId = requestDto.EncounterId,
            JsonNote = jsonData,
            IsAutoSaved = requestDto.IsAutoSaved ?? false
        };
    }
}