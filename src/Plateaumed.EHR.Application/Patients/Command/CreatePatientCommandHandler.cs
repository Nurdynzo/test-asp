using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.PatientProfile;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Patients.Dtos;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.Patients.Command;

public class CreatePatientCommandHandler : ICreatePatientCommandHandler
{
    private readonly IRepository<Patient,long> _patientRepository;
    private readonly IRepository<PatientCodeMapping,long> _patientCodeMappingRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly IUnitOfWorkManager _unitOfWork;
    private readonly IRepository<Wallet,long> _patientWalletRepository;
    private readonly IAbpSession _abpSession;
    private readonly IRepository<ReviewDetailsHistoryState,long> _reviewDetailsHistoryStateRepository;

    public CreatePatientCommandHandler(
        IObjectMapper objectMapper, 
        IRepository<PatientCodeMapping, long> patientCodeMappingRepository,
        IRepository<Patient, long> patientRepository,
        IUnitOfWorkManager unitOfWork,
        IRepository<Wallet, long> patientWalletRepository, 
        IAbpSession abpSession,
        IRepository<ReviewDetailsHistoryState, long> reviewDetailsHistoryStateRepository)
    {
        _objectMapper = objectMapper;
        _patientCodeMappingRepository = patientCodeMappingRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
        _patientWalletRepository = patientWalletRepository;
        _abpSession = abpSession;
        _reviewDetailsHistoryStateRepository = reviewDetailsHistoryStateRepository;
    }

    public async Task<CreateOrEditPatientDto> Handle(CreateOrEditPatientDto request, long facilityId)
    {
        CheckValidation(request);

        var patient = _objectMapper.Map<Patient>(request);

        patient.UuId = Guid.NewGuid();

        if (facilityId <= 0)
        {

            throw new UserFriendlyException("Current user facility is not set");
        }

        await CheckPatientCodeExistsInFacility(request.PatientCode, facilityId);

        var patientCode = new PatientCodeMapping
        {
            PatientCode = request.PatientCode,
            FacilityId = facilityId,
        };

    

        patient.PatientCodeMappings.Add(patientCode);

        await _patientRepository.InsertAsync(patient);

        await _unitOfWork.Current.SaveChangesAsync();

        await CreatePatientWallet(patient);
        await CreateStateForReviewDetailsHistory(patient);
        request.Id = patient.Id;
        return request;

    }

    private async Task CreateStateForReviewDetailsHistory(Patient patient)
    {
        var reviewDetailsHistoryStates = new ReviewDetailsHistoryState
        {
            PatientId = patient.Id
        };
        await _reviewDetailsHistoryStateRepository.InsertAsync(reviewDetailsHistoryStates);
        await _unitOfWork.Current.SaveChangesAsync();
    }

    private async Task CreatePatientWallet(Patient patient)
    {
        var wallet = new Wallet()
        {
            PatientId = patient.Id,
            Balance = new Money(0),
            TenantId = _abpSession.TenantId
        };
        await _patientWalletRepository.InsertAsync(wallet);
        await _unitOfWork.Current.SaveChangesAsync();
    }

    private static void CheckValidation(CreateOrEditPatientDto input)
    {
        if (string.IsNullOrEmpty(input.PatientCode))
        {

            throw new UserFriendlyException("Patient code is required");
        }

        if (input.DateOfBirth > DateTime.Now)
        {
            throw new UserFriendlyException("Date of birth cannot be greater than today");
        }
    }
    private async Task CheckPatientCodeExistsInFacility(string patientCode, long facilityId)
    {
        var isPatientCodeExistInFacility = await _patientCodeMappingRepository.FirstOrDefaultAsync(
            x => x.FacilityId == facilityId && x.PatientCode.Equals(patientCode));
        if (isPatientCodeExistInFacility != null)
        {
            throw new UserFriendlyException("Patient code already exist in this facility");
        }
    }
}