using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Diagnoses;

namespace Plateaumed.EHR.Test.Base.TestData
{
    public class TestDiagnosisBuilder
    {
        private readonly int _tenantId;
        private readonly long _patientId;
        private long _sctId;
        private Diagnosis _diagnosis;
        private string _note;


        public TestDiagnosisBuilder(int tenantId, long patientId)
        {
            _tenantId = tenantId;
            _patientId = patientId;

            SetDefaults();
        }

        public TestDiagnosisBuilder Create(int tenantId, long patientId)
        {
            return new TestDiagnosisBuilder(tenantId, patientId);
        }

        public TestDiagnosisBuilder WithNote(string notes)
        {
            _note = notes;
            return this;
        }


        public Diagnosis Build()
        {
            _diagnosis = new Diagnosis
            {
                TenantId = _tenantId,
                PatientId = _patientId,
                Notes = _note,
                Sctid = _sctId
            };

            return _diagnosis;
        }

        public Diagnosis Save(EHRDbContext context)
        {
            var diagnosis = context.Diagnosis.Add(Build()).Entity;
            context.SaveChanges();
            return diagnosis;
        }


        private void SetDefaults()
        {
            _note = "Default Diagnosis Note";
            _sctId = 1000000;
        }
    }
}