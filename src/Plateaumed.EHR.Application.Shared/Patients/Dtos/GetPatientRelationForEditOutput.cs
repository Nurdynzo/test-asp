using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Patients.Dtos
{
    public class GetPatientRelationForEditOutput
    {
        public CreateOrEditPatientRelationDto PatientRelation { get; set; }

        public string PatientPatientCode { get; set; }
    }
}
