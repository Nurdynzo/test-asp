using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using NUglify.Helpers;
using Plateaumed.EHR.PatientProfile.Abstraction;
using Plateaumed.EHR.PatientProfile.Dto;
namespace Plateaumed.EHR.PatientProfile.Command
{
    public class SavePatientFamilyHistoryCommandHandler : ISavePatientFamilyHistoryCommandHandler
    {
        private readonly IRepository<PatientFamilyHistory,long> _patientFamilyHistoryRepository;
        private readonly IRepository<PatientFamilyMembers,long> _patientFamilyMembersRepository;
        private readonly IObjectMapper _objectMapper;
        public SavePatientFamilyHistoryCommandHandler(
            IRepository<PatientFamilyHistory, long> patientFamilyHistoryRepository,
            IObjectMapper objectMapper,
            IRepository<PatientFamilyMembers, long> patientFamilyMembersRepository)
        {
            _patientFamilyHistoryRepository = patientFamilyHistoryRepository;
            _objectMapper = objectMapper;
            _patientFamilyMembersRepository = patientFamilyMembersRepository;
        }
        public async Task<PatientFamilyHistoryDto> Handle(PatientFamilyHistoryDto request)
        {
            if (request.Id is null or <= 0)
            {
                var patientFamilyHistory = _objectMapper.Map<PatientFamilyHistory>(request);
                patientFamilyHistory.PatientFamilyMembers = _objectMapper.Map<List<PatientFamilyMembers>>(request.FamilyMembers);
                patientFamilyHistory.PatientFamilyMembers.ForEach(x => x.PatientId = request.PatientId);
                request.Id = await _patientFamilyHistoryRepository.InsertAndGetIdAsync(patientFamilyHistory)
                    .ConfigureAwait(false);
                return request;
            }

            var newPatientFamilyMembers =
                from member in request.FamilyMembers
                where member.Id is null or <= 0
                select _objectMapper.Map<PatientFamilyMembers>(member);
            foreach (PatientFamilyMembers patientFamilyMember in newPatientFamilyMembers)
            {
                patientFamilyMember.PatientFamilyHistoryId = request.Id.GetValueOrDefault();
                patientFamilyMember.PatientId = request.PatientId;
                await _patientFamilyMembersRepository.InsertAsync(patientFamilyMember).ConfigureAwait(false);
            }
            var existPatientFamilyHistory = await _patientFamilyHistoryRepository
                .GetAll().Include(x => x.PatientFamilyMembers)
                .FirstOrDefaultAsync(x => x.Id == request.Id.GetValueOrDefault())
                .ConfigureAwait(false);
            existPatientFamilyHistory.IsFamilyHistoryKnown = request.IsFamilyHistoryKnown;
            existPatientFamilyHistory.TotalNumberOfSiblings = request.TotalNumberOfSiblings;
            existPatientFamilyHistory.TotalNumberOfMaleSiblings = request.TotalNumberOfMaleSiblings;
            existPatientFamilyHistory.TotalNumberOfFemaleSiblings = request.TotalNumberOfFemaleSiblings;
            existPatientFamilyHistory.TotalNumberOfChildren = request.TotalNumberOfChildren;
            existPatientFamilyHistory.TotalNumberOfMaleChildren = request.TotalNumberOfMaleChildren;
            existPatientFamilyHistory.TotalNumberOfFemaleChildren = request.TotalNumberOfFemaleChildren;
            foreach (var familyMember in request.FamilyMembers)
            {
                var member = await _patientFamilyMembersRepository.GetAll()
                    .SingleOrDefaultAsync(x => x.Id == familyMember.Id);
                if (member is not null)
                {
                    member.IsAlive = (familyMember?.IsAlive).GetValueOrDefault();
                    member.SeriousIllnesses = familyMember.SeriousIllnesses;
                    member.Relationship = familyMember.Relationship;
                    member.CausesOfDeath = familyMember.CausesOfDeath;
                    member.AgeAtDeath = familyMember.AgeAtDeath;
                    member.AgeAtDiagnosis = familyMember.AgeAtDiagnosis;

                    await _patientFamilyMembersRepository.UpdateAsync(member).ConfigureAwait(false);
                }
            }

            await _patientFamilyHistoryRepository.UpdateAsync(existPatientFamilyHistory).ConfigureAwait(false);

            return request;
        }
    }
}
