using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
using Plateaumed.EHR.Patients;

namespace Plateaumed.EHR.PatientProfile.Command;

public class CreatePatientTravelHistoryCommandHandler : ICreatePatientTravelHistoryCommandHandler
{
    private readonly IRepository<PatientTravelHistory,long> _patientTravelHistoryRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly IUnitOfWorkManager _unitOfWork;

    public CreatePatientTravelHistoryCommandHandler(
        IRepository<PatientTravelHistory, long> patientTravelHistoryRepository,
        IObjectMapper objectMapper,
        IUnitOfWorkManager unitOfWork)
    {
        _patientTravelHistoryRepository = patientTravelHistoryRepository;
        _objectMapper = objectMapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(List<CreatePatientTravelHistoryCommand> request)
    {
        //Delete already existing travel history before create or update
        var initialTravelHistory = await _patientTravelHistoryRepository.GetAll()
            .Where(x => x.PatientId == request[0].PatientId)
            .ToListAsync();
        if(initialTravelHistory != null || initialTravelHistory.Count > 0)
        {
            foreach (var entry in initialTravelHistory)
            {
                await _patientTravelHistoryRepository.DeleteAsync(entry);
            }
        }
        var travelHistories = request.Select(t => _objectMapper.Map<PatientTravelHistory>(t));
        await _patientTravelHistoryRepository.InsertRangeAsync(travelHistories);
        await _unitOfWork.Current.SaveChangesAsync();
    }
}