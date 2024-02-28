using System.Linq;
using Abp.Domain.Repositories;
using Plateaumed.EHR.Patients.Abstractions;

namespace Plateaumed.EHR.Patients.Query.BaseQueryHelper;

public class PatientBaseQuery : IPatientBaseQuery
{
    private readonly IRepository<Patient, long> _patientRepository;
    private readonly IRepository<PatientCodeMapping, long> _patientCodeMappingRepository;
    
    public PatientBaseQuery(IRepository<Patient, long> patientRepository, IRepository<PatientCodeMapping, long> patientCodeMappingRepository)
    {
        _patientRepository = patientRepository;
        _patientCodeMappingRepository = patientCodeMappingRepository;
    }
    
    public IQueryable<Patient> GetPatientByCodeBaseQuery(string patientCode)
    {
        var output = (from p in _patientRepository.GetAll()
            join m in _patientCodeMappingRepository.GetAll() on p.Id equals m.PatientId
            where m.PatientCode == patientCode
            select p);

        return output;
    }
}