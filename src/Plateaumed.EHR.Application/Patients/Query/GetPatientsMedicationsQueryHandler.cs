using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Patients.Abstractions;
using Plateaumed.EHR.Patients.Dtos;

namespace Plateaumed.EHR.Patients.Query
{
    public class GetPatientsMedicationsQueryHandler : IGetPatientsMedicationsQueryHandler
    {
        private readonly IRepository<Patient, long> _patientRepository;
        private readonly IRepository<PatientCodeMapping, long> _patientCodeMapping;
        private readonly IRepository<AllInputs.Medication, long> _medications;

        public GetPatientsMedicationsQueryHandler(
            IRepository<Patient, long> patientRepository,
            IRepository<PatientCodeMapping, long> patientCodeMapping,
             IRepository<AllInputs.Medication, long> medications
            )
        {
            _patientRepository = patientRepository;
            _patientCodeMapping = patientCodeMapping;
            _medications = medications;
          
        }
        public async Task<GetPatientsMedicationsResponse> Handle(long patientId, long facilityId)
        {
            await ValidateInput(patientId);

            var query = await (from p in _patientRepository.GetAll().Where(x => x.Id == patientId)
                               join pc in _patientCodeMapping.GetAll() on p.Id equals pc.PatientId
                               where pc.FacilityId  == facilityId
                               join m in _medications.GetAll() on p.Id equals m.PatientId into pandm
                               select new GetPatientsMedicationsResponse
                               {
                                   PatientId = p.Id,
                                   Age = $"{DateTime.Now.Year - p.DateOfBirth.Year}yrs",
                                   FirstName = p.FirstName,
                                   LastName = p.LastName,
                                   MiddleName = p.MiddleName ?? "",
                                   Gender = p.GenderType.ToString(),
                                   PatientCode = pc.PatientCode ?? "",
                                   PictureUrl = p.PictureUrl ?? "",
                                   PatientMedications = pandm.Select(x => new MedicationsListDto
                                   {
                                       Direction = x.Direction,
                                       DoseUnit = x.DoseUnit,
                                       Duration = x.Duration,
                                       Frequency = x.Frequency,
                                       ProductName = x.ProductName
                                   }).ToList()
                               }).FirstOrDefaultAsync();
            return query;
        }

        private async Task ValidateInput(long patientId)
        {
            _ = await _patientRepository.GetAll().Where(x => x.Id == patientId).FirstOrDefaultAsync() ?? throw new UserFriendlyException("Patient not found");
        }
    }
}

