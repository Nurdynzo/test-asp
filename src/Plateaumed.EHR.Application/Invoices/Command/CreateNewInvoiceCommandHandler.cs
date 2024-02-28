using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using NUglify.Helpers;
using Plateaumed.EHR.Encounters;
using Plateaumed.EHR.Encounters.Dto;
using Plateaumed.EHR.Invoices.Abstraction;
using Plateaumed.EHR.Invoices.Dtos;
using Plateaumed.EHR.Misc;
using Plateaumed.EHR.Patients;
using Plateaumed.EHR.Utility;

namespace Plateaumed.EHR.Invoices.Command;

/// <inheritdoc />
public class CreateNewInvoiceCommandHandler : ICreateNewInvoiceCommandHandler
{
    private readonly IRepository<Invoice, long> _invoiceRepository;
    private readonly IRepository<PatientAppointment, long> _patientAppointmentRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly IUnitOfWorkManager _unitOfWork;
    private readonly IEncounterManager _encounterManager;
    private readonly IAbpSession _abpSession;

    /// <summary>
    /// Constructor for the <see cref="CreateNewInvoiceCommandHandler"/>
    /// </summary>
    /// <param name="invoiceRepository"></param>
    /// <param name="objectMapper"></param>
    /// <param name="patientAppointmentRepository"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="encounterManager"></param>
    /// <param name="abpSession"></param>
    public CreateNewInvoiceCommandHandler(IRepository<Invoice, long> invoiceRepository,
       IObjectMapper objectMapper,
       IRepository<PatientAppointment, long> patientAppointmentRepository,
       IUnitOfWorkManager unitOfWork,
       IEncounterManager encounterManager,
       IAbpSession abpSession)
    {
        _invoiceRepository = invoiceRepository;
        _objectMapper = objectMapper;
        _patientAppointmentRepository = patientAppointmentRepository;
        _unitOfWork = unitOfWork;
        _encounterManager = encounterManager;
        _abpSession = abpSession;
    }

    /// <inheritdoc />
    public async Task<CreateNewInvoiceCommand> Handle(CreateNewInvoiceCommand command, long facilityId)
    {
        if(_abpSession.TenantId == null)
        {
            throw new UserFriendlyException("Tenant Id cannot be null");
        }
        ValidateIfAmountIsCalculatedCorrectly(command);
        var invoice = _objectMapper.Map<Invoice>(command);
        var tenantId = _abpSession.TenantId.Value;

        invoice.FacilityId = facilityId;
        invoice.PatientAppointmentId = command.AppointmentId;
        invoice.InvoiceSource = InvoiceSource.InPatient;
        invoice.TenantId = tenantId;
        invoice.InvoiceItems = command.Items.Select(x => _objectMapper.Map<InvoiceItem>(x)).ToList();
        invoice.InvoiceItems.ForEach(x =>
        {
            x.FacilityId = facilityId;
            x.TenantId = tenantId;
            x.OutstandingAmount = x.SubTotal.Amount.ToMoney(x.SubTotal.Currency);
        });
        if (IsServiceOnCredit(command))
        {
            invoice.IsServiceOnCredit = true;
            await ProcessServiceOnCredit(command, facilityId);
        }

        await _invoiceRepository.InsertAsync(invoice);
        await _unitOfWork.Current.SaveChangesAsync();
        command.Id = invoice.Id;
        return command;
    }

    private async Task ProcessServiceOnCredit(CreateNewInvoiceCommand command, long facilityId)
    {
        var appointment = await _patientAppointmentRepository.GetAll()
                              .Include(x => x.AttendingClinicFk)
                              .FirstOrDefaultAsync(x => x.Id == command.AppointmentId)
                          ?? throw new UserFriendlyException("Appointment not found");

        appointment.Status = AppointmentStatusType.Awaiting_Vitals;
        await _encounterManager.CreateAppointmentEncounter(new CreateAppointmentEncounterRequest
        {
            PatientId = command.PatientId,
            ServiceCentre = appointment.AttendingClinicFk?.ServiceCentre ?? ServiceCentreType.OutPatient,
            AppointmentId = command.AppointmentId,
            FacilityId = facilityId,
            Status = EncounterStatusType.InProgress,
            UnitId = appointment.AttendingClinicFk?.Id
        }).ConfigureAwait(false);
        await _patientAppointmentRepository.UpdateAsync(appointment).ConfigureAwait(false);
    }

    private static void ValidateIfAmountIsCalculatedCorrectly(CreateNewInvoiceCommand command)
    {
        if (command.Items.Count == 0)
        {
            throw new UserFriendlyException("Invoice items cannot be empty");
        }
    }

    private static bool IsServiceOnCredit(CreateNewInvoiceCommand command) => command.IsServiceOnCredit;
}
