using Abp.Application.Services.Dto;
using System;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetAllPatientRelationsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int? RelationshipFilter { get; set; }

        public string FirstNameFilter { get; set; }

        public string MiddleNameFilter { get; set; }

        public string LastNameFilter { get; set; }

        public string PatientPatientCodeFilter { get; set; }
    }
}
