using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using Plateaumed.EHR.Authorization.Users;
using Plateaumed.EHR.Procedures.Abstractions;
using Plateaumed.EHR.Procedures.Dtos;
using Plateaumed.EHR.Url;

namespace Plateaumed.EHR.Procedures.Handlers;

public class EmailStatementCommandHandler : IEmailStatementCommandHandler
{
    public IAppUrlService AppUrlService { get; set; }
    private readonly IUserEmailer _userEmailer;
    private readonly ICreateStatementOfPatientOrNextOfKinOrGuardianCommandHandler _statementOfPatientOrNextOfKinOrGuardianCommandHandler;

    public EmailStatementCommandHandler(IUserEmailer userEmailer, ICreateStatementOfPatientOrNextOfKinOrGuardianCommandHandler statementOfPatientOrNextOfKinOrGuardianCommandHandler)
    {
        _userEmailer = userEmailer;
        _statementOfPatientOrNextOfKinOrGuardianCommandHandler = statementOfPatientOrNextOfKinOrGuardianCommandHandler;
    }
    
    public async Task Handle(EmailStatementDto requestDto, IAbpSession abpSession)
    {
        if(!abpSession.TenantId.HasValue)
            throw new UserFriendlyException($"Tenant Id is required.");

        // TODO: Get the statement and generate the link for that patient
        
        // get email link and send mail to recipients
        var link = AppUrlService.CreateEmailActivationUrlFormat(abpSession.TenantId);
        await _userEmailer.SendEmailConsentDocumentStatementAsync(requestDto.recipientEmails, link, abpSession.TenantId);
    }
}