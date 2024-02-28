using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientDocument.Abstraction
{
    /// <summary>
    /// Handler to get all scanned documents for a patient.
    /// </summary>
    public interface IGetScannedDocumentsByPatientCodeQueryHandler: ITransientDependency
    {
        /// <summary>
        /// Handles getting all scanned documents for a patient.
        /// </summary>
        /// <param name="patientCode"></param>
        Task<List<Guid>> Handle(string patientCode);
    }
}
