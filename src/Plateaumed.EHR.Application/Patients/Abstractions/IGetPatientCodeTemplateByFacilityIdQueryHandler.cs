using Abp.Dependency;
using Plateaumed.EHR.Patients.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plateaumed.EHR.Patients.Abstractions
{
    public interface IGetPatientCodeTemplateByFacilityIdQueryHandler : ITransientDependency
    {
        Task<PatientCodeTemplateDto> Handle(long facilityId);
    }
}
