using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Feeding.Abstractions;
using Plateaumed.EHR.Feeding.Dtos;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.Feeding.Handlers;

public class CreateFeedingCommandHandler : ICreateFeedingCommandHandler
{
    private readonly IRepository<AllInputs.Feeding, long> _feedingRepository;
    private readonly IRepository<Patient, long> _patientRepository;

    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IEncounterManager _encounterManager;

    public CreateFeedingCommandHandler(IRepository<AllInputs.Feeding, long> feedingRepository,
        IRepository<Patient, long> patientRepository, IUnitOfWorkManager unitOfWorkManager, IObjectMapper objectMapper,
        IEncounterManager encounterManager)
    {
        _feedingRepository = feedingRepository;
        _patientRepository = patientRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _encounterManager = encounterManager;
    }

    public async Task<AllInputs.Feeding> Handle(CreateFeedingDto requestDto)
    {
        await _encounterManager.CheckEncounterExists(requestDto.EncounterId);
        var dateOfBirth = (await _patientRepository.GetAsync(requestDto.PatientId)).DateOfBirth;

        if (dateOfBirth <= DateTime.Now.AddYears(-2))
        {
            throw new UserFriendlyException("Patient must not be older then 2 years!");
        }

        //map request data and set other properties
        var feeding = _objectMapper.Map<AllInputs.Feeding>(requestDto);

        await _feedingRepository.InsertAsync(feeding);
        await _unitOfWorkManager.Current.SaveChangesAsync();

        return feeding;
    }
}