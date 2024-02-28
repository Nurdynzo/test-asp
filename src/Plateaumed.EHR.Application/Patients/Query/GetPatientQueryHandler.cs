using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients.Query
{
    public class GetPatientQueryHandler : IGetPatientQueryHandler
    {
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMappingRepository;

        public GetPatientQueryHandler(IRepository<Patient, long> patientRepository,
            IRepository<PatientCodeMapping, long> patientCodeMappingRepository)
        {
            _patientRepository = patientRepository;
            _patientCodeMappingRepository = patientCodeMappingRepository;
        }

        public async Task<CreateOrEditPatientDto> Handle(EntityDto<long> input)
        {
            var output = await (from p in _patientRepository.GetAll()
                                join m in _patientCodeMappingRepository.GetAll() on p.Id equals m.PatientId
                                select new CreateOrEditPatientDto
                                {
                                    Id = p.Id,
                                    PatientCode = m.PatientCode,
                                    Ethnicity = p.Ethnicity,
                                    Religion = p.Religion,
                                    MaritalStatus = p.MaritalStatus,
                                    BloodGroup = p.BloodGroup,
                                    BloodGenotype = p.BloodGenotype,
                                    DateOfBirth = p.DateOfBirth,
                                    PhoneNumber = p.PhoneNumber,
                                    EmailAddress = p.EmailAddress,
                                    GenderType = p.GenderType,
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    MiddleName = p.MiddleName,
                                    Address = p.Address,
                                    Title = p.Title,
                                    CountryId = p.CountryId,
                                    StateOfOriginId = p.StateOfOriginId,
                                    DistrictId = p.DistrictId,
                                    IdentificationCode = p.IdentificationCode,
                                    IdentificationType = p.IdentificationType,
                                    NuclearFamilySize = p.NuclearFamilySize,
                                    NumberOfChildren = p.NumberOfChildren,
                                    NumberOfSiblings = p.NumberOfSiblings,
                                    NumberOfSpouses = p.NumberOfSpouses,
                                    NoOfFemaleChildren = p.NoOfFemaleChildren,
                                    NoOfMaleChildren = p.NoOfMaleChildren,
                                    NoOfFemaleSiblings = p.NoOfFemaleSiblings,
                                    NoOfMaleSiblings = p.NoOfMaleSiblings,
                                    PositionInFamily = p.PositionInFamily,
                                    IsNewToHospital = p.IsNewToHospital,
                                    ProfilePictureId = p.ProfilePictureId,
                                    ProfilePictureUrl = p.PictureUrl
                                }).FirstOrDefaultAsync(x => x.Id == input.Id);


            return output;
        }
    }
}
