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
using Plateaumed.EHR.Procedures;
using Plateaumed.EHR.Vaccines.Abstractions;
using Plateaumed.EHR.Vaccines.Dto;

namespace Plateaumed.EHR.Vaccines.Handlers;

public class CreateVaccinationCommandHandler : ICreateVaccinationCommandHandler
{
    private readonly IRepository<Vaccination, long> _vaccinationRepository; 
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IObjectMapper _objectMapper;
    private readonly IEncounterManager _encounterManager;

    public CreateVaccinationCommandHandler(IRepository<Vaccination, long> vaccinationRepository, IUnitOfWorkManager unitOfWorkManager, 
        IObjectMapper objectMapper, IEncounterManager encounterManager)
    {
        _vaccinationRepository = vaccinationRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _objectMapper = objectMapper;
        _encounterManager = encounterManager;
    }
    
    public async Task Handle(CreateMultipleVaccinationDto requestListDto)
    {
        if(requestListDto.Vaccinations.Count <= 0)
            throw new UserFriendlyException("Add at least one item.");
      
        await _encounterManager.CheckEncounterExists(requestListDto.EncounterId);
        
        foreach (var vaccination in requestListDto.Vaccinations.Select(_objectMapper.Map<Vaccination>))
        {
            vaccination.EncounterId = requestListDto.EncounterId;
            vaccination.ProcedureEntryType = requestListDto.ProcedureEntryType;
            vaccination.ProcedureId = requestListDto.ProcedureId;
            
            await _vaccinationRepository.InsertAsync(vaccination);
        }
        
        await _unitOfWorkManager.Current.SaveChangesAsync(); 
    }
}