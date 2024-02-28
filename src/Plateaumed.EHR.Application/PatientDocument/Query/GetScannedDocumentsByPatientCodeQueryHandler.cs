using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.PatientDocument.Abstraction;
using Plateaumed.EHR.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Plateaumed.EHR.PatientDocument.Query
{
    /// <inheritdoc/>
    public class GetScannedDocumentsByPatientCodeQueryHandler : IGetScannedDocumentsByPatientCodeQueryHandler
    {
        private readonly IRepository<PatientScanDocument, long> _patientScanDocumentRepository;

        /// <param name="patientScanDocumentRepository"></param>
        public GetScannedDocumentsByPatientCodeQueryHandler( 
            IRepository<PatientScanDocument, long> patientScanDocumentRepository)
        {
            _patientScanDocumentRepository = patientScanDocumentRepository;
        }


        /// <inheritdoc/>
        public async Task<List<Guid>> Handle(string patientCode)
        {
           var scannedDocumentIds = await _patientScanDocumentRepository.GetAll()
                .Where(x => x.PatientCode == patientCode && x.IsApproved == true)
                .Select(sc => sc.FileId).ToListAsync();

            return scannedDocumentIds;
        }
    }
}
