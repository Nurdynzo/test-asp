using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;

namespace Plateaumed.EHR.VitalSigns.Handlers;

public class DeletePatientVitalCommandHandler : IDeletePatientVitalCommandHandler
{
    private readonly IRepository<PatientVital, long> _patientVitalRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public DeletePatientVitalCommandHandler(IRepository<PatientVital, long> patientVitalRepository, IUnitOfWorkManager unitOfWorkManager)
    {
        _patientVitalRepository = patientVitalRepository;
        _unitOfWorkManager = unitOfWorkManager;
    }
    
    public async Task Handle(List<long> patientVitalIds)
    {
        foreach (var patientVitalId in patientVitalIds)
        {
            var patientVital = await _patientVitalRepository.GetAsync(patientVitalId) ??
                               throw new UserFriendlyException($"Patient vital not found.");
            
            await _patientVitalRepository.DeleteAsync(patientVital);
        }
        
        // save db changes
        await _unitOfWorkManager.Current.SaveChangesAsync();    
    }
}