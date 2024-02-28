using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.Diagnoses.Abstraction;
using Plateaumed.EHR.Diagnoses.Dto;

namespace Plateaumed.EHR.Diagnoses.Query.BaseQueryHelper
{
    public class DiagnosisBaseQuery : IBaseQuery
    {
        private readonly IRepository<Diagnosis, long> _diagnosisRepository;
        private readonly IAbpSession _abpSession;

        public DiagnosisBaseQuery(
            IRepository<Diagnosis, long> diagnosisRepository,
            IAbpSession abpSession)
        {
            _diagnosisRepository = diagnosisRepository;
            _abpSession = abpSession;
        }
        public IQueryable<Diagnosis> GetPatientDiagnosisBaseQuery(int patientId)
        {
            return _diagnosisRepository
                .GetAll()
                .IgnoreQueryFilters()
                .Where(d => d.PatientId == patientId && d.TenantId == _abpSession.TenantId);
        }
        public IQueryable<Diagnosis> GetPatientDiagnosisWithEncounterId(long patientId, long encounterId)
        {
            return _diagnosisRepository.GetAll().Where(d => d.PatientId == patientId && d.EncounterId == encounterId);
        }

        public string FormatDiagnoses(CreateDiagnosisDto createDiagnosis)
        {
            List<string> clinicalDiagnoses = new List<string>();
            List<string> differentialDiagnoses = new List<string>();

            foreach (var diagn in createDiagnosis.SelectedDiagnoses)
            {
                if (diagn.Type == DiagnosisType.Clinical)
                {
                    clinicalDiagnoses.Add(diagn.Name);
                }
                else if (diagn.Type == DiagnosisType.Differential)
                {
                    differentialDiagnoses.Add(diagn.Name);
                }
            }

            string clinicalDiagnosesText = string.Join(", ", clinicalDiagnoses);
            string differentialDiagnosesText = string.Join(", ", differentialDiagnoses);

            string combinedDiagnoses = clinicalDiagnosesText;

            if (!string.IsNullOrEmpty(differentialDiagnosesText))
            {
                combinedDiagnoses += " R/O " + differentialDiagnosesText;
            }

            return combinedDiagnoses;
        }
    }
}
