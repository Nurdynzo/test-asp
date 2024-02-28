using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUglify.Helpers;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.Test.Base.TestData;
using Plateaumed.EHR.Tests.Invoices.Util;
using Plateaumed.EHR.Utility;
using Plateaumed.EHR.ValueObjects;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Invoices.IntegrationTest;
[Trait("Category","Integration")]
public class InvoiceAppServiceTest : AppTestBase
{
    private readonly IInvoicesAppService _invoicesAppService;

    public InvoiceAppServiceTest()
    {
        _invoicesAppService = Resolve<IInvoicesAppService>();
    }

    [Fact]
    public async Task InvoiceAppService_CreateInvoice_Should_Create_Invoice()
    {
        //arrange
        LoginAsDefaultTenantAdmin();
        long patientId = 0;
        long appointmentId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
            var appointment = GetPatientAppointment(patient);
            context.PatientAppointments.Add(appointment);
            context.SaveChanges();
            appointmentId = appointment.Id;
        });
        
        var command = CommonUtil.GetCreatNewInvoiceCommand(patientId: patientId, appointmentId: appointmentId);
        
        //act
        var invoice = await _invoicesAppService.CreateNewInvoice(command);
        //assert
        invoice.Id.GetValueOrDefault().ShouldBeGreaterThan(0);

    }
    [Fact]
    public async Task InvoiceAppService_GenerateInvoiceCode_Should_Return_Next_Invoice_Number()
    {
        //arrange
        LoginAsDefaultTenantAdmin();
        int tenantId = AbpSession.TenantId.GetValueOrDefault();
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            TestInvoiceBuilder.Create(tenantId).WithInvoiceId("0000000001")
                .WithPatient(patient)
                .WithPatientAppointment(GetPatientAppointment(patient)).Save(context);
            TestInvoiceBuilder.Create(tenantId).WithInvoiceId("0000000002")
                .WithPatient(patient)
                .WithPatientAppointment(GetPatientAppointment(patient)).Save(context);
        });
        //act   
        var invoiceCode = await _invoicesAppService.GenerateInvoiceCode();
        
        //assert
        invoiceCode.ShouldBe("0000000003");

    }
   
    [Fact]
    public async Task InvoiceAppService_GenerateInvoiceCode_For_The_First_InvoiceShould_Return_The_First_Invoice_Number()
    {
        //arrange
        LoginAsDefaultTenantAdmin();
        //act   
        var invoiceCode = await _invoicesAppService.GenerateInvoiceCode();
        //assert
        invoiceCode.ShouldBe("0000000001");
    }

    [Fact]
    public async Task InvoiceAppService_GetInvoiceItemsForPricing_Should_Return_InvoiceItems_With_Valid_Records()
    {
        LoginAsDefaultTenantAdmin();
        int tenantId = AbpSession.TenantId.GetValueOrDefault();
        var request = new GetInvoiceItemPricingRequest();
        UsingDbContext(context =>
        {

            TestInvoiceItemsPricingBuilder.Create(tenantId, null, "Test Item", new Money(100), 1).Save(context);
            TestInvoiceItemsPricingBuilder.Create(tenantId, null, "Test Item 2", new Money(200),1).Save(context);
            TestInvoiceItemsPricingBuilder.Create(tenantId, null, "Test Item 3", new Money(300),1).Save(context);
        });
        //act   
        var itemPricing = await _invoicesAppService.GetInvoiceItemsForPricing(request);
        
        //assert
        itemPricing.Items.Count.ShouldBe(3);
        itemPricing.Items[0].IsGlobal.ShouldBeFalse();
        itemPricing.Items[1].IsGlobal.ShouldBeFalse();
        itemPricing.Items[0].DiscountPercentage.ShouldBe(0);
        itemPricing.Items[2].IsGlobal.ShouldBeFalse();
        itemPricing.Items[2].DiscountPercentage.ShouldBe(0);
    }
    
    [Fact]
    public async Task InvoiceAppService_GetInvoiceItemsForPricing_With_Filter_Should_Return_InvoiceItems_With_Filter_Parameter()
    {
        LoginAsDefaultTenantAdmin();
        int tenantId = AbpSession.TenantId.GetValueOrDefault();
        var request = new GetInvoiceItemPricingRequest {Filter = "Test Item 2"};
        UsingDbContext(context =>
        {
            context.SaveChanges();
            TestInvoiceItemsPricingBuilder.Create(tenantId, null, "Test Item", new Money(100),1).Save(context);
            TestInvoiceItemsPricingBuilder.Create(tenantId, null, "Test Item 2", new Money(200),1).Save(context);
            TestInvoiceItemsPricingBuilder.Create(tenantId, null, "Test Item 3", new Money(300),1).Save(context);
        });
        //act   
        var itemPricing = await _invoicesAppService.GetInvoiceItemsForPricing(request);
        
        //assert
        itemPricing.Items.Count.ShouldBe(1);
        itemPricing.Items[0].IsGlobal.ShouldBeFalse();
        itemPricing.Items[0].DiscountPercentage.ShouldBe(0);
       
    }

    [Fact]
    public async Task InvoiceAppService_GetPatientTotalSummaryHeader_Should_Return_Patient_Payment_Summary_Header()
    {
        // arrange
        LoginAsDefaultTenantAdmin();
        int tenantId = AbpSession.TenantId.GetValueOrDefault();
        long patientId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
            
            var appointment = GetPatientAppointment(patient);
            context.PatientAppointments.Add(appointment);
            context.SaveChanges();

            var invoice = CommonUtil.GetInvoicesForPaymentSummaryAsQueryable(patientId, appointment.Id,tenantId).ToList()[0];
            context.Invoices.Add(invoice);
            context.SaveChanges();
            CommonUtil.GetInvoiceItemsForPaymentSummaryAsQueryable(false,invoice.Id,tenantId)
                .ForEach(x=> context.InvoiceItems.Add(x));

            context.SaveChanges();
            
           
            context.PaymentActivityLogs.AddRange(CommonUtil.GetPaymentActivityLogAsQueryableForIntegrationTest(tenantId).ToList());
            context.SaveChanges();
        });
        
        // act
        var result = await _invoicesAppService.GetPatientTotalSummaryHeader(patientId);
        
        // assert
        result.ShouldNotBeNull();
        result.TotalAmount.ShouldBe(new MoneyDto(){ Amount = 600.00M, Currency = "NGN" });
        result.ToTalPaid.ShouldBe(new MoneyDto(){ Amount = 300.00M, Currency = "NGN" });
        result.TotalOutstanding.ShouldBe(new MoneyDto(){ Amount = 300.00M, Currency = "NGN"});
        result.TotalTopUp.ShouldBe(new MoneyDto(){ Amount = 400.00M, Currency = "NGN"});
        result.ItemsCounts.ShouldBe(1);
    }
    
    [Fact]
    public async Task InvoiceAppService_PayInvoicesHandler_GivenValidRequests_ShouldCompletePayment()
    {
        // arrange
        LoginAsDefaultTenantAdmin();
        int tenantId = AbpSession.TenantId.GetValueOrDefault();
        long patientId = 0;
        long walletId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;

            var wallet = CommonUtil.GetNewWallet(2200, patientId,tenantId);
            context.Wallets.Add(wallet);
            context.SaveChanges();
            walletId = wallet.Id;

            var appointment = GetPatientAppointment(patient);
            context.PatientAppointments.Add(appointment);
            context.SaveChanges();

            var invoices = CommonUtil.GetInvoicesToBePaidForAsQueryable(tenantId, patientId, appointment.Id).ToList();
            context.Invoices.AddRange(invoices);
            context.SaveChanges();
        });
        var request = CommonUtil.GetWalletFundingRequestsDto(patientId);

        // act
        await _invoicesAppService.FundAndFinalize(request);

        // assert
        var invoices = new List<Invoice>();
        var wallet = new Wallet();
        var invoiceItems = new List<InvoiceItem>();
        UsingDbContext(context =>
        {
            invoices = context.Invoices.ToList();
            wallet = context.Wallets.Find(walletId);
            invoiceItems = context.InvoiceItems.ToList();
        });

        var firstInvoice = invoices[0];
        var secondInvoice = invoices[1];

        wallet.Balance.ShouldBe(new Money{ Amount = 1600, Currency = "NGN"});
        invoices.Count.ShouldBe(3);
        firstInvoice.PaymentStatus.ShouldBe(PaymentStatus.Paid);
        firstInvoice.TotalAmount.ShouldBe(firstInvoice.AmountPaid);
        firstInvoice.OutstandingAmount.ShouldBe(new Money(0.00M));
        secondInvoice.PaymentStatus.ShouldBe(PaymentStatus.Paid);
        secondInvoice.TotalAmount.ShouldBe(secondInvoice.AmountPaid);
        secondInvoice.OutstandingAmount.ShouldBe(new Money(0.00M));
        invoiceItems.Count.ShouldBe(6);
        invoiceItems.ForEach(i=> i.Status.ShouldBe(InvoiceItemStatus.Paid));
    }

    [Fact]
    public async Task InvoicesAppService_ProcessPendingCancelRequest_GivenValid_Approval_Requests_ShouldCancelPayment()
    {
        // arrange
        LoginAsDefaultTenantAdmin();
        int tenantId = AbpSession.TenantId.GetValueOrDefault();
        long patientId = 0;
        long facilityId = 1;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
            var appointment = GetPatientAppointment(patient);
            context.PatientAppointments.Add(appointment);
            context.SaveChanges();
            var invoice = GetInvoice(patientId, tenantId, appointment.Id);
            context.Invoices.Add(invoice);
            context.SaveChanges();
            context.InvoiceCancelRequests.AddRange(
                GetInvoiceCancelRequests(patientId, tenantId, invoice.Id, facilityId));
            context.SaveChanges();
        });
        var request = new ApproveCancelInvoiceCommand
        {
            IsApproved = true,
            PatientId = patientId
        };
        //act
        await _invoicesAppService.ProcessPendingCancelRequest(request);
        
        //assert
        var invoiceCancelRequests = new List<InvoiceCancelRequest>();
 
        var invoiceItems = new List<InvoiceItem>();
        UsingDbContext(context =>
        {
            invoiceCancelRequests = context.InvoiceCancelRequests.Where(x=>x.PatientId == patientId).ToList();
            var invoiceIds = invoiceCancelRequests.Select(x=>x.InvoiceItemId).ToArray();
            invoiceItems = context.InvoiceItems.Where(x=> invoiceIds.Contains(x.Id)).ToList();
        });
        
        invoiceCancelRequests.Count.ShouldBe(2);
        invoiceItems.Count.ShouldBe(2);
        invoiceCancelRequests[0].Status.ShouldBe(InvoiceCancelStatus.Approved);
        invoiceCancelRequests[1].Status.ShouldBe(InvoiceCancelStatus.Approved);
        invoiceItems[0].Status.ShouldBe(InvoiceItemStatus.Cancelled);
        invoiceItems[1].Status.ShouldBe(InvoiceItemStatus.Cancelled);
        
    }

    [Fact]
    public async Task InvoiceAppService_ConvertToInvoice_GivenValidRequests_Should_Convert_To_Invoice()
    {
        // arrange
        LoginAsDefaultTenantAdmin();
        int tenantId = AbpSession.TenantId.GetValueOrDefault();
        long patientId = 0;
        long facilityId = 1;
        long appointmentId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
            var appointment = GetPatientAppointment(patient);
            appointmentId = appointment.Id;
            context.PatientAppointments.Add(appointment);
            context.SaveChanges();
            var invoices = CommonUtil.GetProformaInvoiceAsQueryable(facilityId, patientId, tenantId, appointment.Id).ToList();
            context.Invoices.Add(invoices[0]);
            context.SaveChanges();
        });
        // act
        var request = new ProformaToNewInvoiceRequest()
        {
            Id = 1,
            PatientId = patientId,
            TotalAmount = CommonUtil.GetMoneyDto(178.00M),
            InvoiceNo = "0000001",
        };
        await _invoicesAppService.ConvertProformaIntoInvoice(request);
        // assert
        Invoice invoices = null;
        var paymentActivityLogs = new List<PaymentActivityLog>();
        UsingDbContext(context =>
        {
            invoices = context.Invoices.Include(x=>x.InvoiceItems)
                .FirstOrDefault(x => x.PatientId == request.PatientId && x.FacilityId == facilityId &&
                x.InvoiceType == InvoiceType.InvoiceCreation);
            paymentActivityLogs = context.PaymentActivityLogs.Where(x=>
                x.PatientId==patientId).ToList();
        });
        invoices.ShouldNotBeNull();
        invoices.InvoiceType.ShouldBe(InvoiceType.InvoiceCreation);
        invoices.InvoiceItems.Count.ShouldBe(2);
        invoices.TotalAmount.ShouldBe(new Money(178.00M));
        invoices.PaymentStatus.ShouldBe(PaymentStatus.Unpaid);
        
        paymentActivityLogs.Count.ShouldBe(2);
        paymentActivityLogs[0].TransactionType.ShouldBe(TransactionType.Other);
        paymentActivityLogs[0].TransactionAction.ShouldBe(TransactionAction.CreateInvoice);
        
        
    }

    [Fact]
    public async Task InvoiceAppService_CreateCancelInvoice_GivenValidRequests_Should_Save_Cancel_Request()
    {
        // arrange
        LoginAsDefaultTenantAdmin();
        int tenantId = AbpSession.TenantId.GetValueOrDefault();
        long patientId = 0;
        UsingDbContext(context =>
        {
            var patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            patientId = patient.Id;
            var appointment = GetPatientAppointment(patient);
            context.PatientAppointments.Add(appointment);
            context.SaveChanges();
            var invoices = CommonUtil.GetInvoicesToBePaidForAsQueryable(tenantId, patientId, appointment.Id).ToList();
            context.Invoices.AddRange(invoices);
            context.SaveChanges();

        });
        // act
        var request = new CreateCancelInvoiceCommand()
        {
            PatientId = patientId,
            InvoiceItemsIds = new long []{1,2,3}
        };
        await _invoicesAppService.CreateCancelInvoice(request);
        // assert
        var invoiceItems = new List<InvoiceItem>();
        var invoiceCancel = new List<InvoiceCancelRequest>();
        UsingDbContext(context =>
        {
             invoiceItems = context.InvoiceItems.Where(x => request.InvoiceItemsIds.Contains(x.Id)).ToList();
             invoiceCancel = context.InvoiceCancelRequests.Where(x => x.PatientId == patientId).ToList();
        });
        invoiceItems.Count.ShouldBe(3);
        invoiceCancel.Count.ShouldBe(3);
        invoiceCancel[0].Status.ShouldBe(InvoiceCancelStatus.Pending);
        invoiceCancel[1].Status.ShouldBe(InvoiceCancelStatus.Pending);
        invoiceCancel[2].Status.ShouldBe(InvoiceCancelStatus.Pending);
        invoiceItems[0].Status.ShouldBe(InvoiceItemStatus.AwaitCancellationApproval);
        invoiceItems[1].Status.ShouldBe(InvoiceItemStatus.AwaitCancellationApproval);
        invoiceItems[2].Status.ShouldBe(InvoiceItemStatus.AwaitCancellationApproval);
        
            
        
    }

    #region Private Methods
     private InvoiceCancelRequest[] GetInvoiceCancelRequests(
        long patientId =1, 
        int tenantId = 1, 
        long invoiceId = 1,
        long facilityId = 1)
     {
         
         return new[]
        {
            new InvoiceCancelRequest()
            {
                PatientId = patientId,
                InvoiceId = invoiceId,
                TenantId = tenantId,
                Status = InvoiceCancelStatus.Pending,
                FacilityId = facilityId,
                InvoiceItem = new InvoiceItem
                {
                    Id = 1,
                    TenantId = tenantId,
                    FacilityId = 1,
                    Name = "Test 1",
                    InvoiceId = invoiceId,
                    Quantity = 1,
                    UnitPrice = CommonUtil.GetMoney(),
                    SubTotal = CommonUtil.GetMoney(98),
                    DiscountPercentage = 2,
                    AmountPaid = CommonUtil.GetMoney(0.00M),
                    OutstandingAmount = CommonUtil.GetMoney(0.00M),
                    Status = InvoiceItemStatus.Unpaid
                }
                
            },
            new InvoiceCancelRequest()
            {
                PatientId = patientId,
                InvoiceId = invoiceId,
                TenantId = tenantId,
                Status = InvoiceCancelStatus.Pending,
                FacilityId = facilityId,
                InvoiceItem = new InvoiceItem
                {
                    Id = 2,
                    TenantId = tenantId,
                    FacilityId = 1,
                    Name = "Test 2",
                    InvoiceId = invoiceId,
                    Quantity = 1,
                    UnitPrice = CommonUtil.GetMoney(2),
                    SubTotal = CommonUtil.GetMoney(2),
                    DiscountPercentage = 0,
                    AmountPaid = CommonUtil.GetMoney(0.00M),
                    OutstandingAmount = CommonUtil.GetMoney(0.00M),
                    Status = InvoiceItemStatus.Unpaid
                }
                
            }
        };
     }

     private static Invoice GetInvoice(long patientId, int tenantId, long appointmentId)
     {
         var invoice = new Invoice()
         {
             PatientId = patientId,
             TenantId = tenantId,
             FacilityId = 1,
             InvoiceId = "0000000001",
             AmountPaid = CommonUtil.GetMoney(0.00M),
             OutstandingAmount = CommonUtil.GetMoney(),
             TotalAmount = CommonUtil.GetMoney(),
             PaymentStatus = PaymentStatus.Unpaid,
             CreationTime = DateTime.Today,
             CreatorUserId = 1,
             PaymentType = PaymentTypes.Wallet,
             PatientAppointmentId = appointmentId,
         };
         return invoice;
     }
    private static PatientAppointment GetPatientAppointment(Patient patient)
    {
        return new PatientAppointment()
        {
            Type = AppointmentType.Walk_In,
            Title = "Walk In",
            PatientFk = patient
        };
    }

    private static Patient GetPatient()
    {
        return new Patient
        {
            FirstName = "Test", LastName = "User", EmailAddress = "test@user.com", PhoneNumber = "1234567890",
            GenderType = GenderType.Female, DateOfBirth = DateTime.Now.AddYears(-40), UuId = Guid.NewGuid()
        };
    }
    #endregion
}
