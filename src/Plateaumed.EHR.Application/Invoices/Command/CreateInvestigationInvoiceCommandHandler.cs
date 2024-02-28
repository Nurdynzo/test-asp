using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using NUglify.Helpers;
using Plateaumed.EHR.Investigations;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Invoices.Command
{
    public class CreateInvestigationInvoiceCommandHandler : ICreateInvestigationInvoiceCommandHandler
    {
        private readonly IRepository<Invoice, long> _invoiceRepository;
        private readonly IRepository<PatientEncounter, long> _patientEncounterRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWork;
        private readonly IRepository<InvestigationRequest, long> _investigationRequestRepository;
        public CreateInvestigationInvoiceCommandHandler(
        IRepository<Invoice, long> invoiceRepository,
        IObjectMapper objectMapper,
        IRepository<PatientEncounter, long> patientEncounterRepository,
        IUnitOfWorkManager unitOfWork,
        IRepository<InvestigationRequest, long> investigationRequest)
        {
            _invoiceRepository = invoiceRepository;
            _objectMapper = objectMapper;
            _patientEncounterRepository = patientEncounterRepository;
            _unitOfWork = unitOfWork;
            _investigationRequestRepository = investigationRequest;
        }

        public async Task<CreateNewInvestigationInvoiceCommand> Handle(CreateNewInvestigationInvoiceCommand command, long facilityId)
        {
            ValidateIfAmountIsCalculatedCorrectly(command);
            var invoice = _objectMapper.Map<Invoice>(command);
            
            invoice.FacilityId = facilityId;
            invoice.PatientAppointmentId = command.AppointmentId;
            invoice.PatientEncounterId = command.EncounterId;
            invoice.InvoiceSource = command.InvoiceSource;
            invoice.InvoiceItems = command.Items.Select(x => _objectMapper.Map<InvoiceItem>(x)).ToList();
            invoice.InvoiceItems.ForEach(x =>
            {
                x.FacilityId = facilityId;
                x.OutstandingAmount = x.SubTotal.Amount.ToMoney(x.SubTotal.Currency);
            });

            if (IsServiceOnCredit(command))
            {
                invoice.IsServiceOnCredit = true;
                if (!command.EncounterId.HasValue)
                    await ProcessServiceOnCredit(command, facilityId, GetServiceCenterType(command.InvoiceSource));
            }

            await _invoiceRepository.InsertAsync(invoice);
            await _unitOfWork.Current.SaveChangesAsync();
           
            command.Id = invoice.Id;
            if (command.InvoiceSource is InvoiceSource.Lab && invoice.PatientEncounterId.HasValue)
                await CheckInvestigationRequestForStatusUpdate(invoice, invoice.InvoiceItems.ToList());
            return command;
        }

        private async Task CheckInvestigationRequestForStatusUpdate(Invoice invoice, List<InvoiceItem> items)
        {
            var investigationRequest = await GetRequestWithEncounterId(invoice.PatientEncounterId.Value);

            if (items.Count > 0 && investigationRequest is not null && investigationRequest.Count > 0)
            {
                investigationRequest.ForEach(x =>
                {
                    if (items.Any(i => i.Name.ToLower().Equals(x.Investigation.Name.ToLower())))
                    {
                        x.InvestigationStatus = invoice.IsServiceOnCredit ? InvestigationStatus.Processing : InvestigationStatus.Invoiced;
                        UpdateInvestigationRequest(x);
                    }
                });
            }
        }

        private async void UpdateInvestigationRequest(InvestigationRequest request) => await _investigationRequestRepository.UpdateAsync(request);

        private async Task ProcessServiceOnCredit(CreateNewInvestigationInvoiceCommand command, long facilityId, ServiceCentreType serviceCentre)
        {
            var encounter = new PatientEncounter()
            {
                PatientId = command.PatientId,
                ServiceCentre = serviceCentre,
                FacilityId = facilityId
            };
            await _patientEncounterRepository.InsertAsync(encounter);
            await _unitOfWork.Current.SaveChangesAsync();
            var investigation = await GetRequestWithPatientId(command.PatientId);
            if(investigation is not null)
            {
                investigation.PatientEncounterId = encounter.Id;
                UpdateInvestigationRequest(investigation);
            }
            //TODO: ServiceOnCredit needs to be updated with the corresponding amount in payments
        }

        private async Task<List<InvestigationRequest>> GetRequestWithEncounterId(long encounterId)=>
              await _investigationRequestRepository.GetAll().Include(x => x.Investigation)
                               .Where(x => x.PatientEncounterId.Equals(encounterId))
                               .ToListAsync();

        private async Task<InvestigationRequest> GetRequestWithPatientId(long patientId)
            => await _investigationRequestRepository.GetAll().OrderByDescending(x => x.CreationTime)
            .FirstOrDefaultAsync(x => x.PatientId.Equals(patientId));
        
        private static void ValidateIfAmountIsCalculatedCorrectly(CreateNewInvestigationInvoiceCommand command)
        {
            if (command.Items.Count == 0) throw new UserFriendlyException("Invoice items cannot be empty");
        }

        private static bool IsServiceOnCredit(CreateNewInvestigationInvoiceCommand command) => command.IsServiceOnCredit;

        private static ServiceCentreType GetServiceCenterType(InvoiceSource invoiceSource)
        {
            var serviceCenter = ServiceCentreType.Others;

            switch (invoiceSource)
            {
                case InvoiceSource.AccidentAndEmergency:
                    serviceCenter = ServiceCentreType.AccidentAndEmergency;
                    break;
                case InvoiceSource.InPatient:
                    serviceCenter = ServiceCentreType.InPatient;
                    break;
                case InvoiceSource.Lab:
                    serviceCenter = ServiceCentreType.Laboratory;
                    break;
                case InvoiceSource.OutPatient:
                    serviceCenter = ServiceCentreType.OutPatient;
                    break;
                case InvoiceSource.Pharmacy:
                    serviceCenter = ServiceCentreType.Pharmacy;
                    break;
                default:
                    break;
            }
            return serviceCenter;
        }
    }
}

