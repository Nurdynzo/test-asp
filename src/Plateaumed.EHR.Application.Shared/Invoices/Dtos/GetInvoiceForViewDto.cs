namespace Plateaumed.EHR.Invoices.Dtos
{
    public class GetInvoiceForViewDto
    {
        public InvoiceDto Invoice { get; set; }

        public string PatientPatientCode { get; set; }

        public string PatientAppointmentTitle { get; set; }

    }
}