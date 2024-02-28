using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Plateaumed.EHR.Invoices.Dtos
{
    public class GetInvoiceForEditOutput
    {
        public CreateOrEditInvoiceDto Invoice { get; set; }

        public string PatientPatientCode { get; set; }

        public string PatientAppointmentTitle { get; set; }

    }
}