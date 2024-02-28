using System.Collections.Generic;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.ValueObjects;

namespace Plateaumed.EHR.Test.Base.TestData;

public class TestInvoiceBuilder
{
    private readonly int _tenantId;
    private  Invoice _invoice;
    private Patient _patient;
    private PatientAppointment _patientAppointment;
    private string _invoiceId;
    private readonly List<InvoiceItem> _invoiceItems = new();
    public static TestInvoiceBuilder Create(int tenantId)
    {
        return new TestInvoiceBuilder(tenantId);
    }
        
    private TestInvoiceBuilder(int tenantId)
    {
        _tenantId = tenantId;
    }

    public TestInvoiceBuilder WithPatient(Patient patient)
    {
        _patient = patient;
        return this;
        
    }

    public TestInvoiceBuilder WithInvoiceItem(InvoiceItem invoiceItem)
    {
        _invoiceItems.Add(invoiceItem);
        return this;
        
    }
    public TestInvoiceBuilder WithPatientAppointment(PatientAppointment patientAppointment)
    {
        _patientAppointment = patientAppointment;
        return this;
        
    }

    public TestInvoiceBuilder WithInvoiceId(string invoiceId)
    {
        _invoiceId = invoiceId;
        return this;
        
    }
    public Invoice Build()
    {
        _invoice = new Invoice
        {
            TenantId = _tenantId,
            PatientFk = _patient,
            InvoiceId = _invoiceId,
            PatientAppointmentFk = _patientAppointment,
            TotalAmount = new Money(100),
            DiscountPercentage = new Money(10),
            
        };
        return _invoice;
    }

    public Invoice Save(EHRDbContext context)
    {
        var savedInvoice = context.Invoices.Add(Build()).Entity;
        context.SaveChanges();
        return savedInvoice;
        
    }
}