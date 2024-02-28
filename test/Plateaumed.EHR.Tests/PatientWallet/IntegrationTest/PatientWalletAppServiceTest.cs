
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.PatientWallet;
using Plateaumed.EHR.Tests.PatientWallet.Utils;
using Plateaumed.EHR.ValueObjects;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Plateaumed.EHR.EntityFrameworkCore;
using Plateaumed.EHR.PatientWallet.Dtos.WalletRefund;
using Xunit;

namespace Plateaumed.EHR.Tests.PatientWallet.IntegrationTest
{
    [Trait("Category", "Integration")]
    public class PatientWalletAppServiceTest : AppTestBase
    {
        private readonly IPatientWalletAppService _patientWalletAppService;

        public PatientWalletAppServiceTest()
        {
            _patientWalletAppService = Resolve<IPatientWalletAppService>();
        }

        [Fact]
        public async Task PatientWalletAppService_ApproveWalletFundingRequestsHandler_GivenValidRequests_ShouldTopUpAndCompletePayment()
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

                var wallet = CommonUtils.GetNewWallet(2200, patientId, tenantId);
                context.Wallets.Add(wallet);
                context.SaveChanges();
                walletId = wallet.Id;

                var appointment = GetPatientAppointment(patient);
                context.PatientAppointments.Add(appointment);
                context.SaveChanges();

                var invoices = CommonUtils.GetInvoicesWithItemsAwaitingApprovalAsQueryable(tenantId, patientId, appointment.Id).ToList();
                context.Invoices.AddRange(invoices);
                context.SaveChanges();


                var walletHistories = CommonUtils.GetWalletHistories(patientId, facilityId: 1, walletBalance: wallet.Balance.Amount);
                context.WalletHistory.AddRange(walletHistories);
                context.SaveChanges();
            });
            var request = CommonUtils.GetWalletFundingRequestsForInvoiceItemsAwaitingApproval(patientId);

            // act
            await _patientWalletAppService.ApproveWalletFundingRequests(request);

            // assert
            var invoices = new List<Invoice>();
            var wallet = new Wallet();
            var invoiceItems = new List<InvoiceItem>();
            var walletHistories = new List<WalletHistory>();
            UsingDbContext(context =>
            {
                invoices = context.Invoices.ToList();
                wallet = context.Wallets.Find(walletId);
                invoiceItems = context.InvoiceItems.ToList();
                walletHistories = context.WalletHistory.ToList();
            });

            var firstInvoice = invoices[0];
            var secondInvoice = invoices[1];

            wallet.Balance.ShouldBe(new Money { Amount = 3602, Currency = "NGN" });
            walletHistories[0].Status.ShouldBe(TransactionStatus.Approved);
            walletHistories[1].Status.ShouldBe(TransactionStatus.Approved);
            invoices.Count.ShouldBe(3);
            firstInvoice.PaymentStatus.ShouldBe(PaymentStatus.PartiallyPaid);
            firstInvoice.TotalAmount.ShouldNotBe(firstInvoice.AmountPaid);
            firstInvoice.OutstandingAmount.ShouldBe(new Money(2.00M));
            secondInvoice.PaymentStatus.ShouldBe(PaymentStatus.Paid);
            secondInvoice.TotalAmount.ShouldBe(secondInvoice.AmountPaid);
            secondInvoice.OutstandingAmount.ShouldBe(new Money(0.00M));
            invoiceItems.Count.ShouldBe(6);
            invoiceItems.Count(x => x.Status == InvoiceItemStatus.Paid).ShouldBe(5);
            invoiceItems.Count(x => x.Status == InvoiceItemStatus.Unpaid).ShouldBe(1);
        }

        [Fact]
        public async Task ProcessRefundRequestHandler_GivenApprovedValidRequests_ShouldRefund_And_TopUpWallet()
        {
            // arrange
            LoginAsDefaultTenantAdmin();
            int tenantId = AbpSession.TenantId.GetValueOrDefault();
            long facilityId = 1;
            Patient patient = null;
            UsingDbContext(context => { SeedTestData(context, facilityId, tenantId, out patient); });
            ProcessRefundRequestCommand request = CommonUtils.GetProcessRefundRequestCommand(patientId: patient.Id);
            // act
            await _patientWalletAppService.ProcessRefundRequest(request);
            
            // assert
            Wallet wallet = null;
            List<Invoice> invoice = null;
            List<InvoiceRefundRequest> refundRequests = null;
            List<PaymentActivityLog> paymentActivityLog = null;
            UsingDbContext(context =>
            {
                wallet = context.Wallets.Find(patient.Id);
               invoice = context
                    .Invoices
                    .Include(x=>x.InvoiceItems).Where(x => x.PatientId == patient.Id).ToList();
               refundRequests = context.InvoiceRefundRequests.Where(x=>x.PatientId == patient.Id).ToList();
               paymentActivityLog = context.PaymentActivityLogs.Where(x=>x.PatientId==patient.Id).ToList();
            });
            wallet.ShouldNotBeNull();
            wallet.Balance.ShouldBe(new Money { Amount = 478.00M, Currency = "NGN" });
            invoice.Count.ShouldBe(1);
            invoice.First().InvoiceItems.Count.ShouldBe(3);
            invoice.First().InvoiceItems.ShouldContain(x=> x.Status == InvoiceItemStatus.Refunded);
            refundRequests.Count.ShouldBe(3);
            refundRequests.ShouldContain(x=>x.Status == InvoiceRefundStatus.Approved);
            paymentActivityLog.Count.ShouldBe(3);
            paymentActivityLog.ShouldContain(x=>x.TransactionAction == TransactionAction.ApproveRefund);


        }

        [Fact]
        public async Task ProcessRefundRequestHandler_GivenRejectedValidRequests_ShouldNotRefund_And_NotTopUpWallet()
        {
            // arrange
            LoginAsDefaultTenantAdmin();
            int tenantId = AbpSession.TenantId.GetValueOrDefault();
            long facilityId = 1;
            Patient patient = null;
            UsingDbContext(context => { SeedTestData(context, facilityId, tenantId, out patient); });
            ProcessRefundRequestCommand request =
                CommonUtils.GetProcessRefundRequestCommand(patientId: patient.Id, isApproved: false);
            // act
            await _patientWalletAppService.ProcessRefundRequest(request);

            // assert
            Wallet wallet = null;
            List<Invoice> invoice = null;
            List<InvoiceRefundRequest> refundRequests = null;
            List<PaymentActivityLog> paymentActivityLog = null;
            UsingDbContext(context =>
            {
                wallet = context.Wallets.Find(patient.Id);
                invoice = context
                    .Invoices
                    .Include(x => x.InvoiceItems).Where(x => x.PatientId == patient.Id).ToList();
                refundRequests = context.InvoiceRefundRequests.Where(x => x.PatientId == patient.Id).ToList();
                paymentActivityLog = context.PaymentActivityLogs.Where(x=>x.PatientId==patient.Id).ToList();

            });
            wallet.ShouldNotBeNull();
            wallet.Balance.ShouldBe(new Money { Amount = 100.00M, Currency = "NGN" });
            invoice.Count.ShouldBe(1);
            invoice.First().InvoiceItems.Count.ShouldBe(3);
            invoice.First().InvoiceItems.ShouldContain(x => x.Status == InvoiceItemStatus.Paid);
            refundRequests.Count.ShouldBe(3);
            refundRequests.ShouldContain(x => x.Status == InvoiceRefundStatus.Rejected);
            paymentActivityLog.Count.ShouldBe(3);
            paymentActivityLog.ShouldContain(x=>x.TransactionAction == TransactionAction.RejectRefund);
        }

        [Fact]
        public async Task ProcessRefundRequest_With_Invalid_Amount_To_Refund_Should_Throw_UserFriendlyException()
        {
            // arrange
            LoginAsDefaultTenantAdmin();
            int tenantId = AbpSession.TenantId.GetValueOrDefault();
            long facilityId = 1;
            Patient patient = null;
            UsingDbContext(context => { SeedTestData(context, facilityId, tenantId, out patient); });
            ProcessRefundRequestCommand request = CommonUtils.GetProcessRefundRequestCommand(patientId: patient.Id,amount:300.00M);
            // act & assert
            var message = await Should.ThrowAsync<UserFriendlyException>(
                async () => await _patientWalletAppService.ProcessRefundRequest(request));
            
            message.Message.ShouldBe("Refund amount does not match the total amount to refund");
            
        }

        private static void SeedTestData(EHRDbContext context, long facilityId, int tenantId, out Patient patient)
        {
            
            patient = GetPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
            long patientId = patient.Id;
            var appointment = GetPatientAppointment(patient);
            context.PatientAppointments.Add(appointment);
            context.SaveChanges();
            var wallet = CommonUtils.GetNewWallet(100.00M, patientId, tenantId);
            context.Wallets.Add(wallet);
            context.SaveChanges();
            var invoice = new Invoice()
            {
                PatientId = patientId,
                AmountPaid = new Money(450.00M),
                FacilityId = facilityId,
                TenantId = tenantId,
                InvoiceId = "0000000001",
                TotalAmount = new Money(450.00M),
                PatientAppointmentId = appointment.Id,
                TimeOfInvoicePaid = DateTime.Today.AddDays(-2),
                PaymentStatus = PaymentStatus.Paid,
                InvoiceType = InvoiceType.InvoiceCreation,
                InvoiceSource = InvoiceSource.OutPatient,
                PaymentType = PaymentTypes.Wallet,
                OutstandingAmount = new Money(0.00M)
            };
            context.Invoices.Add(invoice);
            context.SaveChanges();

            var refundRequest = new List<InvoiceRefundRequest>
            {
                new()
                {
                    Id = 1,
                    PatientId = patientId,
                    InvoiceId = invoice.Id,
                    Status = InvoiceRefundStatus.Pending,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    InvoiceItem = new InvoiceItem
                    {
                        Id = 1,
                        InvoiceId = invoice.Id,
                        TenantId = tenantId,
                        Status = InvoiceItemStatus.Paid,
                        AmountPaid = new Money(100.00M),
                        SubTotal = new Money(100.00M),
                        Quantity = 1,
                        Name = "test 1",
                        UnitPrice = new Money(100.00M),
                        OutstandingAmount = new Money(0.00M),
                        FacilityId = facilityId
                    }
                },
                new()
                {
                    Id = 2,
                    PatientId = patientId,
                    InvoiceId = invoice.Id,
                    Status = InvoiceRefundStatus.Pending,
                    TenantId = 1,
                    FacilityId = 1,
                    InvoiceItem = new InvoiceItem
                    {
                        Id = 2,
                        InvoiceId = invoice.Id,
                        TenantId = tenantId,
                        Status = InvoiceItemStatus.Paid,
                        AmountPaid = new Money(200.00M),
                        SubTotal = new Money(200.00M),
                        Quantity = 1,
                        Name = "test 2",
                        UnitPrice = new Money(200.00M),
                        OutstandingAmount = new Money(0.00M),
                        FacilityId = facilityId
                    }
                },
                new()
                {
                    Id = 3,
                    PatientId = patientId,
                    InvoiceId = invoice.Id,
                    Status = InvoiceRefundStatus.Pending,
                    TenantId = tenantId,
                    FacilityId = facilityId,
                    InvoiceItem = new InvoiceItem
                    {
                        Id = 3,
                        InvoiceId = invoice.Id,
                        TenantId = tenantId,
                        Status = InvoiceItemStatus.Paid,
                        AmountPaid = new Money(150.00M),
                        SubTotal = new Money(150.00M),
                        Quantity = 1,
                        Name = "test 3",
                        UnitPrice = new Money(150.00M),
                        OutstandingAmount = new Money(0.00M),
                        FacilityId = facilityId
                    }
                }
            };

            context.InvoiceRefundRequests.AddRange(refundRequest);
            context.SaveChanges();
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
                FirstName = "Test",
                LastName = "User",
                EmailAddress = "test@user.com",
                PhoneNumber = "1234567890",
                GenderType = GenderType.Female,
                DateOfBirth = DateTime.Now.AddYears(-40),
                UuId = Guid.NewGuid()
            };
        }
    }
}
