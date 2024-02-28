using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
namespace Plateaumed.EHR.PatientProfile.Query
{
    public class GetPatientFamilyHistoryQueryHandler : IGetPatientFamilyHistoryQueryHandler
    {
        private readonly IRepository<PatientFamilyHistory, long> _patientFamilyHistoryRepository;
        private readonly IObjectMapper _objectMapper;
        public GetPatientFamilyHistoryQueryHandler(IRepository<PatientFamilyHistory, long> patientFamilyHistoryRepository, IObjectMapper objectMapper)
        {
            _patientFamilyHistoryRepository = patientFamilyHistoryRepository;
            _objectMapper = objectMapper;
        }
        public async Task<PatientFamilyHistoryDto> Handle(long patientId)
        {

            var patientFamilyHistory = await _patientFamilyHistoryRepository.GetAll()
            .Include(x => x.PatientFamilyMembers)
            .FirstOrDefaultAsync(x => x.PatientId == patientId);
            var mappedResponse = _objectMapper.Map<PatientFamilyHistoryDto>(patientFamilyHistory);
            if(patientFamilyHistory != null)
            {
                mappedResponse.FamilyMembers = _objectMapper.Map<List<PatientFamilyMembersDto>>(patientFamilyHistory.PatientFamilyMembers);
            }
            return mappedResponse;
        }
    }
}
