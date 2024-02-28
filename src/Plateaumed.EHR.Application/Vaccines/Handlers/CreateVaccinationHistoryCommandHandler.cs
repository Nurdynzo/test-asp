using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Vaccines.Abstractions;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines.Handlers;

public class CreateVaccinationHistoryCommandHandler : ICreateVaccinationHistoryCommandHandler
{
    private readonly IRepository<VaccinationHistory, long> _vaccinationHistoryRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IEncounterManager _encounterManager;

    public CreateVaccinationHistoryCommandHandler(IRepository<VaccinationHistory, long> vaccinationHistoryRepository, IUnitOfWorkManager unitOfWorkManager, 
        IObjectMapper objectMapper, IEncounterManager encounterManager)
    {
        _vaccinationHistoryRepository = vaccinationHistoryRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _encounterManager = encounterManager;
    }
    
    public async Task Handle(CreateMultipleVaccinationHistoryDto requestListDto)
    {
        if (requestListDto.VaccinationHistory.Count <= 0)
            throw new UserFriendlyException("Add at least one item.");

        if(requestListDto.EncounterId > 0)
            await _encounterManager.CheckEncounterExists(requestListDto.EncounterId);
        foreach (var vaccinationHistory in requestListDto.VaccinationHistory.Select(_objectMapper.Map<VaccinationHistory>))
        {
            if (requestListDto.EncounterId > 0)
                vaccinationHistory.EncounterId = requestListDto.EncounterId;
            // add to list
            await _vaccinationHistoryRepository.InsertAsync(vaccinationHistory);
        }
        await _unitOfWorkManager.Current.SaveChangesAsync();
    }
}