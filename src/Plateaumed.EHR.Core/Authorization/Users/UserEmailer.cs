using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Localization;
using Abp.Net.Mail;
using Plateaumed.EHR.Chat;
using Plateaumed.EHR.Editions;
using Plateaumed.EHR.Localization;
using Plateaumed.EHR.MultiTenancy;
using System.Net.Mail;
using System.Web;
using Abp.Runtime.Security;
using Abp.Timing;
using Plateaumed.EHR.Configuration;
using Plateaumed.EHR.Net.Emailing;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Net.Emailing.ContactSales;
using Microsoft.Extensions.Configuration;

namespace Plateaumed.EHR.Authorization.Users
{
    /// <summary>
    /// Used to send email to users.
    /// </summary>
    public class UserEmailer : EHRServiceBase, IUserEmailer, ITransientDependency
    {
        private readonly IEmailTemplateProvider _emailTemplateProvider;
        private readonly IEmailSender _emailSender;
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly ICurrentUnitOfWorkProvider _unitOfWorkProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ISettingManager _settingManager;
        private readonly EditionManager _editionManager;
        private readonly UserManager _userManager;
        readonly IConfigurationRoot _appConfiguration;


        private const string _placeHolderEmailBody = "{EMAIL_BODY}";

        public UserEmailer(
            IEmailTemplateProvider emailTemplateProvider,
            IEmailSender emailSender,
            IRepository<Tenant> tenantRepository,
            ICurrentUnitOfWorkProvider unitOfWorkProvider,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            EditionManager editionManager,
            UserManager userManager,
            IAppConfigurationAccessor appConfigurationAccessor)
        {
            _emailTemplateProvider = emailTemplateProvider;
            _emailSender = emailSender;
            _tenantRepository = tenantRepository;
            _unitOfWorkProvider = unitOfWorkProvider;
            _unitOfWorkManager = unitOfWorkManager;
            _settingManager = settingManager;
            _editionManager = editionManager;
            _userManager = userManager;
            _appConfiguration = appConfigurationAccessor.Configuration;

        }

        /// <summary>
        /// Send email activation link to user's email address.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="link">Email activation link</param>
        /// <param name="plainPassword">
        /// Can be set to user's plain password to include it in the email.
        /// </param>
        public virtual async Task SendEmailActivationLinkAsync(User user, string link, string plainPassword = null)
        {
            await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
            {
                if (user.EmailConfirmationCode.IsNullOrEmpty())
                {
                    throw new Exception("EmailConfirmationCode should be set in order to send email activation link.");
                }

                var subject = L("EHRAccountCreation_Email_Subject");
                var mailMessage = ComposeNewTenantEmailActivationMessage(user, link);

                await SendEmailAsync(user.EmailAddress, subject, mailMessage);
            });
        }

        /// <summary>
        /// Send email activation link to user's email address.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="link">Email activation link</param>
        /// <param name="plainPassword">
        /// Can be set to user's plain password to include it in the email.
        /// </param>
        public virtual async Task SendEmailActivationLinkToStaffAsync(User user, string link, string plainPassword = null)
        {
            await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
            {
                if (user.EmailConfirmationCode.IsNullOrEmpty())
                {
                    throw new Exception("EmailConfirmationCode should be set in order to send email activation link.");
                }

                var subject = L("EHRStaffCreated_Email_Subject");
                var mailMessage = await ComposeNewStaffEmailMessage(user, link);

                await SendEmailAsync(user.EmailAddress, subject, mailMessage);
            });
        }

        public async Task SendEmailToReceiverOnSharedRegistrationAsync(string emailAddress, string senderName, string tenancyName, int tenantId, string link)
        {
            await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
            {
                var subject = L("SharedRegistrationReceiver_Email_Subject");
                var mailMessage = ComposeEmailToReceiverOnSharedRegistration(senderName, tenancyName, tenantId, link);

                await SendEmailAsync(emailAddress, subject, mailMessage);
            });
        }

        public async Task SendEmailToSenderOnSharedRegistrationAsync(User user, string recipients, string tenancyName)
        {
            await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
            {
                var subject = L("SharedRegistrationSender_Email_Subject");
                var mailMessage = ComposeEmailToSenderOnSharedRegistration(user, recipients, tenancyName);

                await SendEmailAsync(user.EmailAddress, subject, mailMessage);
            });
        }


        public virtual async Task SendEmailConsentDocumentStatementAsync(List<string> recipients, string link, int? tenantId)
        {
            await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
            {
                if (recipients.Count <= 0)
                {
                    throw new Exception("At least one recipient email is required.");
                }

                var subject = L("EHRConsentDocument_Email_Subject");
                var mailMessage = ComposeNewConsentDocumentStatement(link, tenantId);

                foreach (var recipient in recipients)
                    await SendEmailAsync(recipient, subject, mailMessage);
            });
        }

        /// <summary>
        /// Compose the email to be sent to a user's email to activate a tenant.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="link">Email Call To Action link</param>
        private StringBuilder ComposeNewTenantEmailActivationMessage(User user, string link)
        {
            if (user.EmailConfirmationCode.IsNullOrEmpty())
            {
                throw new Exception("EmailConfirmationCode should be set in order to send email activation link.");
            }

            var callToActionText = L("EHRAccountCreation_Email_CompleteRegistration");
            var greetings = L("EHRAccountCreation_Email_Greetings", user.Name, user.Surname);
            var content = L("EHRAccountCreation_Email_AccountCreationDetails");
            var tenantRegistrationInstruction = L("EHRAccountCreation_ClickTheLinkOrCopyAndPasteTheLinkBelowIntoYourBrowser");

            link = link.Replace("{userId}", user.Id.ToString());
            link = link.Replace("{confirmationCode}", Uri.EscapeDataString(user.EmailConfirmationCode));

            if (user.TenantId.HasValue)
            {
                link = link.Replace("{tenantId}", user.TenantId.ToString());
            }
            link = EncryptQueryParameters(link);

            var mailMessage = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(user.TenantId));

            var mailBody = new StringBuilder();

            string emailBodyHtml = "<div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\"><div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"><tr><td align=\"left\"><div style=\"line-height: 28px;\"><span style=\"font-family: 'General Sans', sans-serif; font-size: 20px; color: #051438;\">"
                            + greetings
                            + "</span></div></td></tr></table></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>"
                            + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div><div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"><div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"><tr><td align=\"left\"><div style=\"line-height: 24px;\"><span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                            + content
                            + "<br></span></div></td></tr></table></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>"
                            + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div><div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 24px 0px;\"><div><!--[if mso]><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"#\" style=\"height:42px;v-text-anchor:middle;width:224;\" arcsize=\"24%\" strokecolor=\"#0b0c7d\" fillcolor=\"#0b0c7d\"><w:anchorlock></w:anchorlock><center style=\"color: #ffffff; font-size: 18px; font-weight: normal; font-family: 'General Sans', sans-serif;\">"
                            + callToActionText
                            + "</center></v:roundrect><![endif]--><a target=\"_blank\" href=\""
                            + link
                            + "\" style=\"background-color:#0b0c7d;font-size:18px;font-weight:normal;line-height:42px;width:222px;border:1px solid #0b0c7d;color:#ffffff;border-radius:10px;display:inline-block;font-family:General Sans, sans-serif;text-align:center;text-decoration:none;-webkit-text-size-adjust:none;mso-hide:all\">"
                            + callToActionText
                            + "</a></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>"
                            + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div><div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"><div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"><tr><td align=\"left\"><div style=\"line-height: 24px;\"><span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                            + tenantRegistrationInstruction
                            + "</span></div></td></tr></table></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>"
                            + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div><div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"><div style=\"line-height: 24px; font-style: italic;\"><span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                            + link
                            + "</span></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>";

            mailBody.AppendLine(
                emailBodyHtml
                );

            mailMessage = ReplaceEmailBodyItems(mailMessage, mailBody);

            return mailMessage;
        }
        
        /// <summary>
        /// Compose the email to be sent to a new staff's email
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="link">Email Call To Action link</param>
        private async Task<StringBuilder> ComposeNewStaffEmailMessage(User user, string link)
        {
            if (user.PasswordResetCode.IsNullOrEmpty())
            {
                throw new Exception("Email ResetCode should be set in order to send email activation link.");
            }

            var expirationHours = await _settingManager.GetSettingValueAsync<int>(
                AppSettings.UserManagement.Password.PasswordResetCodeExpirationHours
                );
            var greetings = L("EHRStaffCreated_Email_Greetings", user.Title, user.Name, user.Surname);
            var content = L("EHRStaffCreated_Email_Details");
            var contentDetail = L("EHRStaffCreated_Email_Content_Details");
            var helpAndSupportText = L("FacilityUser_Email_HelpAndSupport", "https://"+L("Host_ContactUs_URL"), L("Host_ContactUs_URL"), "https://"+L("Host_UserGuides_URL"), L("Host_UserGuides_URL"));
            var welcomeToNeoEHR = L("FacilityUser_Email_WelcomeToNeoEHR");

            link = link.Replace("{userId}", user.Id.ToString());
            link = link.Replace("{resetCode}", Uri.EscapeDataString(user.PasswordResetCode));

            if (user.TenantId.HasValue)
            {
                link = link.Replace("{tenantId}", user.TenantId.ToString());
            }
            var expireDate = Uri.EscapeDataString(Clock.Now.AddHours(expirationHours).ToString(EHRConsts.DateTimeOffsetFormat));

            link = link.Replace("{expireDate}", expireDate);
            link = EncryptQueryParameters(link);

            var mailMessage = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(user.TenantId));
            var mailBody = new StringBuilder();

            string emailBodyHtml = "<div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"left\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\" valign=\"top\"> <div> <div> <img src=\"img/i-1536040299.png\" width=\"600\" alt=\"\" border=\"0\" style=\"display: block; max-width: 600px; width: 100%;\" class=\"w600px\"> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"191\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 191px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 191px;\"> <tr><td align=\"center\" valign=\"top\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 28px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 20px; color: #051438;\">"
                + greetings
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + content
                + "<br><br>"
                + contentDetail
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td> </tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"center\" valign=\"middle\" style=\"padding: 2px 6px; border-radius: 10px;\"> <div> <div style=\"line-height: 20px; word-break: break-all;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #0b0c7d; text-decoration: underline;\">"
                + link
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> <div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\"><span style=\"text-decoration: none;\">"
                + helpAndSupportText
                + "</span></span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> <div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + welcomeToNeoEHR
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> <div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> ";

            mailBody.AppendLine(
                emailBodyHtml
                );

            mailMessage = ReplaceEmailBodyItems(mailMessage, mailBody);

            return mailMessage;
        }

        private StringBuilder ComposeEmailToReceiverOnSharedRegistration(string senderName,string tenancyName, int? tenantId, string link)
        {
            var greetings = L("SharedRegistrationReceiver_Email_Greetings"); ;
            var confirmationMessage = L("SharedRegistrationReceiver_Email_ConfirmationMessage", senderName, tenancyName);
            var alternateButtonClickInfo = L("SharedRegistrationReceiver_Email_ClickInfo");
            var createFirstTimePasswordText = L("SharedRegistrationReceiver_Email_FirstTimePasswordText");
            var ignoreText = L("SharedRegistrationReceiver_Email_IgnoreText");
            var thankYouMessage = L("SharedRegistrationReceiver_Email_ThankYouMessage");
            var hostSignature = L("SharedRegistrationReceiver_Email_Sincerely") + ",<br>" + L("ThePlateaumedTeam");

            var mailMessage = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(tenantId));
            var mailBody = new StringBuilder();

            string emailBodyHtml = "<div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" bgcolor=\"#edf0f8\" style=\"padding: 10px;\"> <div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"233\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 233px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 233px;\"> <tr><td align=\"center\" valign=\"middle\" bgcolor=\"#edf0f8\" style=\"padding: 10px 0px;\"> <div> <div> <a href=\"{HOST_WEBSITE}/\" target=\"_blank\" style=\"font-family: Arial, sans-serif; font-size: 14px; color: #000000;\"><img src=\"img/i-130062515.png\" width=\"233\" alt=\"\" border=\"0\" style=\"display: block; max-width: 233px; width: 100%;\" class=\"w233px\"></a> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"left\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\" valign=\"top\"> <div> <div> <img src=\"img/i-1536040299.png\" width=\"600\" alt=\"\" border=\"0\" style=\"display: block; max-width: 600px; width: 100%;\" class=\"w600px\"> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"190\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 190px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 190px;\"> <tr><td align=\"center\" valign=\"top\"> <div> <div style=\"line-height: 28px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 20px; color: #051438;\">"
                + greetings
                + ",</span> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + confirmationMessage
                + ".</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 24px 0px;\"> <div> <div> <!--[if mso]> <v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"#\" style=\"height:42px;v-text-anchor:middle;width:222px;\" arcsize=\"24%\" strokecolor=\"#0b0c7d\" strokeweight=\"1px\" fillcolor=\"#0b0c7d\"> <w:anchorlock></w:anchorlock> <v:stroke joinstyle=\"round\" dashstyle=\"solid\"></v:stroke> <center style=\"color: #ffffff; font-size: 18px; font-weight: normal; font-family: 'General Sans', sans-serif;\">Complete registration</center> </v:roundrect> <![endif]--> <a target=\"_blank\""
                + "href=\"" + link + "\""
                + "style=\"background-color:#0b0c7d;font-size:18px;font-weight:normal;line-height:42px;width:224px;border: 1px solid #0b0c7d;color:#ffffff;border-radius:10px;display:inline-block;font-family:General Sans, sans-serif;text-align:center;text-decoration:none;-webkit-text-size-adjust:none;box-sizing:border-box;mso-hide:all\">Complete registration</a> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + alternateButtonClickInfo
                + ":</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div style=\"line-height: 24px; font-style: italic;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #0b0c7d; text-decoration: underline;\">"
                + link + "</span> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + createFirstTimePasswordText + ".<br>"
                + ignoreText
                + ".</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + thankYouMessage
                + ".</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + hostSignature
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> ";

            mailBody.AppendLine(
                emailBodyHtml
                );

            mailMessage = ReplaceEmailBodyItems(mailMessage, mailBody);

            return mailMessage;
        }

        private StringBuilder ComposeEmailToSenderOnSharedRegistration(User user, string recipients, string tenancyName)
        {
            var greetings = L("SharedRegistrationSender_Email_Greetings", user.Name, user.Surname);
            var confirmationMessage = L("SharedRegistrationSender_Email_ConfirmationMessage", recipients, tenancyName);
            var hostContactUsURL = L("Host_ContactUs_URL");
            var visitSupportCenter = L("SharedRegistrationSender_Email_VisitSupportCenter", hostContactUsURL, hostContactUsURL);
            var mailMessage = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(user.TenantId));
            var mailBody = new StringBuilder();

            string emailBodyHtml = "<div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" bgcolor=\"#edf0f8\" style=\"padding: 10px;\"> <div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"233\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 233px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 233px;\"> <tr><td align=\"center\" valign=\"middle\" bgcolor=\"#edf0f8\" style=\"padding: 10px 0px;\"> <div> <div> <a href=\"{HOST_WEBSITE}/\" target=\"_blank\" style=\"font-family: Arial, sans-serif; font-size: 14px; color: #000000;\"><img src=\"img/i-130062515.png\" width=\"233\" alt=\"\" border=\"0\" style=\"display: block; max-width: 233px; width: 100%;\" class=\"w233px\"></a> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"left\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\" valign=\"top\"> <div> <div> <img src=\"img/i-1536040299.png\" width=\"600\" alt=\"\" border=\"0\" style=\"display: block; max-width: 600px; width: 100%;\" class=\"w600px\"> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"152\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 152px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 152px;\"> <tr><td align=\"center\" valign=\"top\"> <div> <div style=\"line-height: 28px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 20px; color: #051438;\">"
                + greetings
                + ",</span> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + confirmationMessage
                + ".</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + visitSupportCenter
                + "<span style=\"text-decoration: none;\"> so that we may assist you to rectify this.</span></span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">Thank you for using Neo.</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">Sincerely,<br>The PlateauMed team</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> ";

            mailBody.AppendLine(
                emailBodyHtml
                );

            mailMessage = ReplaceEmailBodyItems(mailMessage, mailBody);

            return mailMessage;
        }

        private StringBuilder ComposeNewConsentDocumentStatement(string link, int? tenantId)
        {
            link = EncryptQueryParameters(link);
            var greetings = L("EHRConsentDocument_Email_Greetings");
            var contentDetails = L("EHRConsentDocument_Email_Details");
            var hostContactUsURL = L("Host_ContactUs_URL");
            var mailMessage = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(tenantId));
            var mailBody = new StringBuilder();

            string emailBodyHtml = "<div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" bgcolor=\"#edf0f8\" style=\"padding: 10px;\"> <div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"233\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 233px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 233px;\"> <tr><td align=\"center\" valign=\"middle\" bgcolor=\"#edf0f8\" style=\"padding: 10px 0px;\"> <div> <div> <a href=\"{HOST_WEBSITE}/\" target=\"_blank\" style=\"font-family: Arial, sans-serif; font-size: 14px; color: #000000;\"><img src=\"img/i-130062515.png\" width=\"233\" alt=\"\" border=\"0\" style=\"display: block; max-width: 233px; width: 100%;\" class=\"w233px\"></a> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"left\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\" valign=\"top\"> <div> <div> <img src=\"img/i-1536040299.png\" width=\"600\" alt=\"\" border=\"0\" style=\"display: block; max-width: 600px; width: 100%;\" class=\"w600px\"> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"152\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 152px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 152px;\"> <tr><td align=\"center\" valign=\"top\"> <div> <div style=\"line-height: 28px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 20px; color: #051438;\">"
                + greetings
                + ",</span> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + contentDetails
                + ".</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\"><span style=\"text-decoration: none;\">"
                + link
                + "</span><a href=\""
                + hostContactUsURL
                + "\" target=\"_blank\" style=\"color: #0b0c7d; text-decoration: underline;\">"
                + hostContactUsURL
                + "</a><span style=\"text-decoration: none;\"> so that we may assist you to rectify this.</span></span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">Thank you for using Neo.</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">Sincerely,<br>The PlateauMed team</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> ";
            
            mailBody.AppendLine(emailBodyHtml);
            mailMessage = ReplaceEmailBodyItems(mailMessage, mailBody);

            return mailMessage;
        }

        /// <summary>
        /// Sends a password reset link to user's email.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="link">Reset link</param>
        public async Task SendPasswordResetLinkAsync(User user, string link = null)
        {
            await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
            {
                if (user.PasswordResetCode.IsNullOrEmpty())
                {
                    throw new Exception("PasswordResetCode should be set in order to send password reset link.");
                }

                var subject = L("PasswordResetEmail_Title");
                var mailMessage = await ComposeResetEmailMessage(user, link);

                await SendEmailAsync(user.EmailAddress, subject, mailMessage);
            });
        }

        /// <summary>
        /// Sends email activation link to facility user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="link"></param>
        public async Task SendEmailActivationLinkToFacilityUserAsync(User user, string targetEditionName, string link = null)
        {
            await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
            {

                var subject = L("FacilityUser_Email_Subject");
                var mailMessage = ComposeFacilityEmailMessage(user, targetEditionName, link);

                await SendEmailAsync(user.EmailAddress, subject, mailMessage);
            });
        }


        public async Task TryToSendChatMessageMail(User user, string senderUsername, string senderTenancyName,
            ChatMessage chatMessage)
        {
            try
            {
                var emailTemplate = GetTitleAndSubTitle(user.TenantId, L("NewChatMessageEmail_Title"),
                    L("NewChatMessageEmail_SubTitle"));
                var mailMessage = new StringBuilder();

                mailMessage.AppendLine("<b>" + L("Sender") + "</b>: " + senderTenancyName + "/" + senderUsername +
                                       "<br />");
                mailMessage.AppendLine("<b>" + L("Time") + "</b>: " +
                                       chatMessage.CreationTime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss") +
                                       " UTC<br />");
                mailMessage.AppendLine("<b>" + L("Message") + "</b>: " + chatMessage.Message + "<br />");
                mailMessage.AppendLine("<br />");

                await ReplaceBodyAndSendAsync(user.EmailAddress, L("NewChatMessageEmail_Subject"), emailTemplate,
                    mailMessage);
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
            }
        }

        public async Task TryToSendSubscriptionExpireEmail(int tenantId, DateTime utcNow)
        {
            try
            {
                using (_unitOfWorkManager.Begin())
                {
                    using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                    {
                        var tenantAdmin = await _userManager.GetAdminAsync();
                        if (tenantAdmin == null || string.IsNullOrEmpty(tenantAdmin.EmailAddress))
                        {
                            return;
                        }

                        var hostAdminLanguage = await _settingManager.GetSettingValueForUserAsync(
                            LocalizationSettingNames.DefaultLanguage, tenantAdmin.TenantId, tenantAdmin.Id);
                        var culture = CultureHelper.GetCultureInfoByChecking(hostAdminLanguage);
                        var emailTemplate = GetTitleAndSubTitle(tenantId, L("SubscriptionExpire_Title"),
                            L("SubscriptionExpire_SubTitle"));
                        var mailMessage = new StringBuilder();

                        mailMessage.AppendLine("<b>" + L("Message") + "</b>: " + L("SubscriptionExpire_Email_Body",
                            culture, utcNow.ToString("yyyy-MM-dd") + " UTC") + "<br />");
                        mailMessage.AppendLine("<br />");

                        await ReplaceBodyAndSendAsync(tenantAdmin.EmailAddress, L("SubscriptionExpire_Email_Subject"),
                            emailTemplate, mailMessage);
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
            }
        }

        public async Task TryToSendSubscriptionAssignedToAnotherEmail(int tenantId, DateTime utcNow,
            int expiringEditionId)
        {
            try
            {
                using (_unitOfWorkManager.Begin())
                {
                    using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                    {
                        var tenantAdmin = await _userManager.GetAdminAsync();
                        if (tenantAdmin == null || string.IsNullOrEmpty(tenantAdmin.EmailAddress))
                        {
                            return;
                        }

                        var hostAdminLanguage = await _settingManager.GetSettingValueForUserAsync(
                            LocalizationSettingNames.DefaultLanguage, tenantAdmin.TenantId, tenantAdmin.Id);
                        var culture = CultureHelper.GetCultureInfoByChecking(hostAdminLanguage);
                        var expringEdition = await _editionManager.GetByIdAsync(expiringEditionId);
                        var emailTemplate = GetTitleAndSubTitle(tenantId, L("SubscriptionExpire_Title"),
                            L("SubscriptionExpire_SubTitle"));
                        var mailMessage = new StringBuilder();

                        mailMessage.AppendLine("<b>" + L("Message") + "</b>: " +
                                               L("SubscriptionAssignedToAnother_Email_Body", culture,
                                                   expringEdition.DisplayName, utcNow.ToString("yyyy-MM-dd") + " UTC") +
                                               "<br />");
                        mailMessage.AppendLine("<br />");

                        await ReplaceBodyAndSendAsync(tenantAdmin.EmailAddress, L("SubscriptionExpire_Email_Subject"),
                            emailTemplate, mailMessage);
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
            }
        }

        public async Task TryToSendFailedSubscriptionTerminationsEmail(List<string> failedTenancyNames, DateTime utcNow)
        {
            try
            {
                var hostAdmin = await _userManager.GetAdminAsync();
                if (hostAdmin == null || string.IsNullOrEmpty(hostAdmin.EmailAddress))
                {
                    return;
                }

                var hostAdminLanguage =
                    await _settingManager.GetSettingValueForUserAsync(LocalizationSettingNames.DefaultLanguage,
                        hostAdmin.TenantId, hostAdmin.Id);
                var culture = CultureHelper.GetCultureInfoByChecking(hostAdminLanguage);
                var emailTemplate = GetTitleAndSubTitle(null, L("FailedSubscriptionTerminations_Title"),
                    L("FailedSubscriptionTerminations_SubTitle"));
                var mailMessage = new StringBuilder();

                mailMessage.AppendLine("<b>" + L("Message") + "</b>: " + L("FailedSubscriptionTerminations_Email_Body",
                    culture, string.Join(",", failedTenancyNames), utcNow.ToString("yyyy-MM-dd") + " UTC") + "<br />");
                mailMessage.AppendLine("<br />");

                await ReplaceBodyAndSendAsync(hostAdmin.EmailAddress, L("FailedSubscriptionTerminations_Email_Subject"),
                    emailTemplate, mailMessage);
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
            }
        }

        public async Task TryToSendSubscriptionExpiringSoonEmail(int tenantId, DateTime dateToCheckRemainingDayCount)
        {
            try
            {
                await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
                {
                    using (UnitOfWorkManager.Current.SetTenantId(tenantId))
                    {
                        var tenantAdmin = await _userManager.GetAdminAsync();
                        if (tenantAdmin == null || string.IsNullOrEmpty(tenantAdmin.EmailAddress))
                        {
                            return;
                        }

                        var tenantAdminLanguage = await _settingManager.GetSettingValueForUserAsync(
                            LocalizationSettingNames.DefaultLanguage,
                            tenantAdmin.TenantId,
                            tenantAdmin.Id
                        );

                        var culture = CultureHelper.GetCultureInfoByChecking(tenantAdminLanguage);

                        var emailTemplate = GetTitleAndSubTitle(
                            null,
                            L("SubscriptionExpiringSoon_Title"),
                            L("SubscriptionExpiringSoon_SubTitle")
                        );

                        var mailMessage = new StringBuilder();

                        mailMessage.AppendLine("<b>" + L("Message") + "</b>: " +
                                               L("SubscriptionExpiringSoon_Email_Body", culture,
                                                   dateToCheckRemainingDayCount.ToString("yyyy-MM-dd") + " UTC") +
                                               "<br />");
                        mailMessage.AppendLine("<br />");

                        await ReplaceBodyAndSendAsync(
                            tenantAdmin.EmailAddress,
                            L("SubscriptionExpiringSoon_Email_Subject"),
                            emailTemplate,
                            mailMessage
                        );
                    }
                });
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
            }
        }

        public void TryToSendPaymentNotCompletedEmail(int tenantId, string urlToPayment)
        {
            try
            {
                _unitOfWorkManager.WithUnitOfWork(() =>
                {
                    using (UnitOfWorkManager.Current.SetTenantId(tenantId))
                    {
                        var tenantAdmin = _userManager.GetAdmin();
                        if (tenantAdmin == null || string.IsNullOrEmpty(tenantAdmin.EmailAddress))
                        {
                            return;
                        }

                        var emailTemplate = GetTitleAndSubTitle(null, L("SubscriptionPaymentNotCompleted_Title"),
                            L("SubscriptionPaymentNotCompleted_SubTitle"));
                        var mailMessage = new StringBuilder();

                        mailMessage.AppendLine(L("SubscriptionPaymentNotCompleted_Email_Body", urlToPayment) +
                                               "<br />");
                        mailMessage.AppendLine("<br />");

                        ReplaceBodyAndSend(tenantAdmin.EmailAddress, L("SubscriptionPaymentNotCompleted_Email_Subject"),
                            emailTemplate, mailMessage);
                    }
                });
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
            }
        }

        private string GetTenancyNameOrNull(int? tenantId)
        {
            if (tenantId == null)
            {
                return null;
            }

            using (_unitOfWorkProvider.Current.SetTenantId(null))
            {
                return _tenantRepository.Get(tenantId.Value).TenancyName;
            }
        }

        private StringBuilder GetTitleAndSubTitle(int? tenantId, string title, string subTitle)
        {
            var emailTemplate = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(tenantId));
            emailTemplate.Replace("{EMAIL_TITLE}", title);
            emailTemplate.Replace("{EMAIL_SUB_TITLE}", subTitle);

            return emailTemplate;
        }

        private async Task SendEmailAsync(string emailAddress, string subject, StringBuilder mailMessage)
        {
            await _emailSender.SendAsync(new MailMessage
            {
                To = { emailAddress },
                Subject = subject,
                Body = mailMessage.ToString(),
                IsBodyHtml = true
            });
        }

        private async Task ReplaceBodyAndSendAsync(string emailAddress, string subject, StringBuilder emailTemplate,
            StringBuilder mailMessage)
        {
            try
            {
                emailTemplate.Replace(_placeHolderEmailBody, mailMessage.ToString());
                await SendEmailAsync(emailAddress, subject, mailMessage);
            }
            catch
            {
                throw new Exception();
            }
        }

        private void SendEmail(string emailAddress, string subject, StringBuilder emailTemplate)
        {
            _emailSender.SendAsync(new MailMessage
            {
                To = { emailAddress },
                Subject = subject,
                Body = emailTemplate.ToString(),
                IsBodyHtml = true
            });
        }

        private void ReplaceBodyAndSend(string emailAddress, string subject, StringBuilder emailTemplate,
            StringBuilder mailMessage)
        {
            emailTemplate.Replace(_placeHolderEmailBody, mailMessage.ToString());
            SendEmail(emailAddress, subject, emailTemplate);
        }

        /// <summary>
        /// Sends sales team RFQ (Request for Quote) email.
        /// </summary>
        /// <param name="facility">Facility</param>
        /// <param name="request">EHR Feature requests from host customers</param>
        /// <param name="userFullName">Fullname of the requester of RFQ</param>
        public async Task SendRFQToSalesTeam(Facility facility, QuotationRequest request, string userFullName)
        {
            await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
            {
                var subject = L("QuotationRequestSalesTeam_Email_Subject", userFullName);
                var salesTeamEamil = _appConfiguration.GetValue<string>("HostInformation:ContactSalesEmail");
                var mailMessage = ComposeUserRFQToSalesTeamMessage(userFullName, facility, request);

                await SendEmailAsync(salesTeamEamil, subject, mailMessage);
            });
        }

        /// <summary>
        /// Sends customers RFQ (Request for Quote) email.
        /// </summary>
        /// <param name="facility">User</param>
        /// <param name="request">EHR Feature requests from host customers</param>
        /// <param name="userFullName">User full name</param>
        public async Task SendRFQDetailsToUsers(Facility facility, QuotationRequest request, string userFullName)
        {
            await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
            {
                var subject = L("QuotationRequestUser_Email_Subject");
                var mailMessage = ComposeRFQDetailsToUserMessage(userFullName, facility, request);

                await SendEmailAsync(facility.EmailAddress, subject, mailMessage);
            });
        }

        private StringBuilder ComposeUserRFQToSalesTeamMessage(string userFullName, Facility facility, QuotationRequest quotationRequest)
        {
            var greetings = L("QuotationRequestSalesTeam_Email_Greetings");
            var introMessage = L("QuotationRequestSalesTeam_Email_IntroductionMessage", userFullName, facility.Name);
            var hospitalInformation = L("HospitalInformation");
            var hospitalName = L("HospitalName");
            var hospitalLocation = L("HospitalLocation");
            var hospitalCityCountry = L("HospitalCityCountry", facility.City, facility.Country);

            var hospitalEmail = L("HospitalEmail");
            var businessInfo = L("BusinessInfo");
            var facilityType = L("TypeOfFacility");
            var contactName = L("ContactName");
            var contactNumber = L("ContactNumber");
            var requestDetails = L("CustomizationRequestDetails");
            var modulesRequested = L("ModulesRequested");
            var additionalComments = L("AdditionalComments");
            var closingMessage = L("QuotationRequestSalesTeam_Email_ClosingMessage");

            var quotation = ConvertQuotationItemsToString(quotationRequest);
            var mailMessage = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(facility.TenantId));
            var mailBody = new StringBuilder();

            mailBody.AppendLine(
                "<div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\"><div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"><tr><td align=\"left\"><div style=\"line-height: 28px;\"><span style=\"font-family: 'General Sans', sans-serif; font-size: 20px; color: #051438;\">"
                + greetings
                + "</span></div></td></tr></table></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div><div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"><div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"><tr><td align=\"left\"><div style=\"line-height: 24px;\"><span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + introMessage
                + "<br></span></div></td></tr></table></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div><div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"><div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"><tr><td align=\"left\"><div style=\"line-height: 24px;\"><span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + $"{hospitalInformation}:"
                + "<ul>"
                + $"<li>{hospitalName}: {facility.Name}</li>"
                + $"<li>{hospitalLocation}: {hospitalCityCountry}</li>"
                + $"<li>{hospitalEmail}: {facility.Name}</li>"
                + $"<li>{businessInfo}: {facility.Name}</li>"
                + $"<li>{facilityType}: {facility.TypeFk.Name}</li>"
                + $"<li>{contactName}: {userFullName}</li>"
                + $"<li>{contactNumber}: {facility.PhoneNumber}</li>"
                + "</ul>"
                + $"{requestDetails}:"
                + "<ul>"
                + $"<li>{modulesRequested}: {quotation}</li>"
                + $"<li>{additionalComments}: {quotationRequest.Comment}</li>"
                + "</ul>"
                + "<br></span></div></td></tr></table></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div><div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"><div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"><tr><td align=\"left\"><div style=\"line-height: 24px;\"><span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + closingMessage
                + "<br></span></div></td></tr></table></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>"
                );

            mailMessage = ReplaceEmailBodyItems(mailMessage, mailBody);

            return mailMessage;
        }

        private StringBuilder ComposeRFQDetailsToUserMessage(string userFullName, Facility facility, QuotationRequest quotationRequest)
        {
            var greetings = L("QuotationRequestUser_Email_Greetings", userFullName);
            var introMessage = L("QuotationRequestUser_Email_IntroductionMessage");
            var requestDetails = L("CustomizationRequestDetails");
            var modulesRequested = L("ModulesRequested");
            var additionalComments = L("AdditionalComments");
            var closingMessage = L("QuotationRequestUser_Email_ClosingMessage");
            var hostSignature = L("BestRegards") + ",<br/>" + L("ThePlateaumedTeam");

            var mailMessage = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(facility.TenantId));
            var mailBody = new StringBuilder();
            var quotation = ConvertQuotationItemsToString(quotationRequest).ToString();

            mailBody.AppendLine(
                "<div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\"><div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"><tr><td align=\"left\"><div style=\"line-height: 28px;\"><span style=\"font-family: 'General Sans', sans-serif; font-size: 20px; color: #051438;\">"
                + greetings
                + "</span></div></td></tr></table></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div><div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"><div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"><tr><td align=\"left\"><div style=\"line-height: 24px;\"><span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + introMessage
                + "<br></span></div></td></tr></table></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div><div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"><div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"><tr><td align=\"left\"><div style=\"line-height: 24px;\"><span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + $"{requestDetails}:"
                + "<ul>"
                + $"<li>{modulesRequested}: {quotation}</li>"
                + $"<li>{additionalComments}: {quotationRequest.Comment}</li>"
                + "</ul>"
                + "<br></span></div></td></tr></table></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div><div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"><div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"><tr><td align=\"left\"><div style=\"line-height: 24px;\"><span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + closingMessage
                + "<br></span></div></td></tr></table></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div><div><!--[if (gte mso 9)|(IE)]><table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"><tr><td><![endif]--><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"><div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"><tr><td align=\"left\"><div style=\"line-height: 24px;\"><span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + hostSignature
                + "<br></span></div></td></tr></table></div></td></tr></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></div>"
                );

            mailMessage = ReplaceEmailBodyItems(mailMessage, mailBody);

            return mailMessage;
        }

        private static StringBuilder ConvertQuotationItemsToString(QuotationRequest quotationRequest)
        {
            var quotation = new StringBuilder();

            var arr = quotationRequest.Quotation;

            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i].Value)
                {
                    var m = arr.Count - 1 == i ? "" : ", ";
                    var name = arr[i].Name + m;
                    quotation.AppendLine(name);
                }
            }

            return quotation;
        }

        /// <summary>
        /// Returns link with encrypted parameters
        /// </summary>
        /// <param name="link"></param>
        /// <param name="encrptedParameterName"></param>
        /// <returns></returns>
        private static string EncryptQueryParameters(string link, string encrptedParameterName = "c")
        {
            if (!link.Contains('?'))
            {
                return link;
            }

            var basePath = link.Substring(0, link.IndexOf('?'));
            var query = link.Substring(link.IndexOf('?')).TrimStart('?');

            return basePath + "?" + encrptedParameterName + "=" +
                   HttpUtility.UrlEncode(SimpleStringCipher.Instance.Encrypt(query));
        }

        private async Task<StringBuilder> ComposeResetEmailMessage(User user, string link)
        {
            var greetings = L("PasswordReset_Email_Greetings");
            var firstParagraph = L("PasswordReset_Email_Purpose");
            var clickingTheLink = L("PasswordReset_Email_IfClickingTheLink");
            var safelyIgnore = L("PasswordReset_Email_SafelyIgnore");
            var nonDisclosure = L("PasswordResult_Email_NonDisclosure");
            var forHelpAndSupport = L("PasswordResult_Email_ForHelpAndSupport");
            var appreciation = L("PasswordResult_Email_Appreciation");
            var hostContactUsURL = L("Host_ContactUs_URL");
            // var resetCode = L("PasswordReset_Email_ResetCode", user.PasswordResetCode); // TODO should be fixed later. The link does not work
            var resetCode = "";

            link = link.Replace("{userId}", user.Id.ToString());
            link = link.Replace("{resetCode}", Uri.EscapeDataString(user.PasswordResetCode));

            var expirationHours = await _settingManager.GetSettingValueAsync<int>(
            AppSettings.UserManagement.Password.PasswordResetCodeExpirationHours
            );
            if (user.PasswordResetCode.IsNullOrEmpty())
            {
                throw new Exception("PasswordResetCode should be set in order to send password reset link.");
            }

            var expireDate = Uri.EscapeDataString(Clock.Now.AddHours(expirationHours).ToString(EHRConsts.DateTimeOffsetFormat));

            link = link.Replace("{expireDate}", expireDate);

            if (user.TenantId.HasValue)
            {
                link = link.Replace("{tenantId}", user.TenantId.ToString());
            }

            link = EncryptQueryParameters(link);

            var mailMessage = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(user.TenantId));
            var mailBody = new StringBuilder();

            string emailBodyHtml = "<div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" bgcolor=\"#edf0f8\" style=\"padding: 10px;\"> <div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"233\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 233px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 233px;\"> <tr><td align=\"center\" valign=\"middle\" bgcolor=\"#edf0f8\" style=\"padding: 10px 0px;\"> <div> <div> <a href=\"/\" target=\"_blank\" style=\"font-family: Arial, sans-serif; font-size: 14px; color: #000000;\"><img src=\"img/i-130062515.png\" width=\"233\" alt=\"\" border=\"0\" style=\"display: block; max-width: 233px; width: 100%;\" class=\"w233px\"></a> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> <div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"left\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\" valign=\"top\"> <div> <div> <img src=\"img/i-1536040299.png\" width=\"600\" alt=\"\" border=\"0\" style=\"display: block; max-width: 600px; width: 100%;\" class=\"w600px\"> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> <div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> "
                + "<div> <!--[if (gte mso 9)|(IE)]> <table width=\"263\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 263px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 263px;\"> <tr><td align=\"center\" valign=\"top\"> <div> <div style=\"line-height: 28px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 20px; color: #051438;\">"
                + greetings 
                + "</span> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + firstParagraph 
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #0b0c7d;\"><a href=\""
                + link
                + "\" target=\"_blank\" style=\"color: #0b0c7d; text-decoration: underline; word-break: break-all;\">"
                + link
                + "</a></span> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + resetCode
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + clickingTheLink 
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + safelyIgnore 
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + nonDisclosure 
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\"><span style=\"text-decoration: none;\">"
                + forHelpAndSupport 
                + "</span><a href=\""
                + hostContactUsURL
                + "\" target=\"_blank\" style=\"color: #0b0c7d; text-decoration: underline;\">"
                + hostContactUsURL
                + "</a><span style=\"text-decoration: none;\">.</span></span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + appreciation 
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">Sincerely,<br>The PlateauMed team</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> ";


            mailBody.AppendLine(
                emailBodyHtml
                );

            mailMessage = ReplaceEmailBodyItems(mailMessage, mailBody);

            return mailMessage;
        }


        private StringBuilder ComposeFacilityEmailMessage(User user, string targetEditionName, string link)
        {
            string greetings = L("FacilityUser_Email_Greetings", user.Name, user.Surname);
            string message = L("FacilityUser_Email_Success_Message", targetEditionName);
            string helpAndSupportText = L("FacilityUser_Email_HelpAndSupport");
            string welcomeToNeoEHR = L("FacilityUser_Email_WelcomeToNeoEHR");

            var mailMessage = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(user.TenantId));
            var mailBody = new StringBuilder();

            string emailBodyHtml = "<div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" bgcolor=\"#edf0f8\" style=\"padding: 10px;\"> <div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"233\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 233px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 233px;\"> <tr><td align=\"center\" valign=\"middle\" bgcolor=\"#edf0f8\" style=\"padding: 10px 0px;\"> <div> <div> <a href=\"{HOST_WEBSITE}/\" target=\"_blank\" style=\"font-family: Arial, sans-serif; font-size: 14px; color: #000000;\"><img src=\"img/i-130062515.png\" width=\"233\" alt=\"\" border=\"0\" style=\"display: block; max-width: 233px; width: 100%;\" class=\"w233px\"></a> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> <div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"left\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\" valign=\"top\"> <div> <div> <img src=\"img/i-1536040299.png\" width=\"600\" alt=\"\" border=\"0\" style=\"display: block; max-width: 600px; width: 100%;\" class=\"w600px\"> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> <div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> "
                + "<div> <!--[if (gte mso 9)|(IE)]> <table width=\"152\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 152px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 152px;\"> <tr><td align=\"center\" valign=\"top\"> <div> <div style=\"line-height: 28px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 20px; color: #051438;\">"
                + greetings 
                + ",</span> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + message 
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 16px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">Please click on your facility url below to access your facility portal:</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div> "
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"center\" valign=\"middle\" style=\"padding: 2px 6px; border-radius: 10px;\"> <div> <div style=\"line-height: 20px; word-break: break-all;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #0b0c7d; text-decoration: underline;\">"
                + link 
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\"><span style=\"text-decoration: none;\">"
                + helpAndSupportText 
                + "</span></span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div>"
                + "<div style=\"height: 16px; line-height: 16px; font-size: 14px;\">&nbsp;</div> <div> <!--[if (gte mso 9)|(IE)]> <table width=\"600\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width: 600px;\"> <tr><td> <![endif]--> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" style=\"max-width: 600px;\"> <tr><td align=\"center\" valign=\"top\" style=\"padding: 10px 0px;\"> <div> <div> <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\"> <tr><td align=\"left\"> <div> <div style=\"line-height: 24px;\"> <span style=\"font-family: 'General Sans', sans-serif; font-size: 16px; color: #051438;\">"
                + welcomeToNeoEHR 
                + "</span> </div> </div> </td></tr> </table> </div> </div> </td></tr> </table> <!--[if (gte mso 9)|(IE)]> </td></tr> </table> <![endif]--> </div>";

            mailBody.AppendLine(
                emailBodyHtml
                );

            mailMessage = mailMessage.Replace(_placeHolderEmailBody, mailBody.ToString());
            mailMessage = ReplaceEmailBodyItems(mailMessage, mailBody);
            return mailMessage;
        }

        private StringBuilder ReplaceEmailBodyItems(StringBuilder mailMessage, StringBuilder mailBody)
        {
            var placeHolderHostRightsReserved = "{HOST_RIGHTS_RESERVED}";
            var placeHolderHostQuestionsOrSupportDetails = "{HOST_QUESTIONS_OR_SUPPORT_DETAILS}";
            var placeHolderHostWebsite = "{HOST_WEBSITE}";
            var rightsReserved = L("AllRightsReserved");
            var hostWebsiteHttps = L("HostWebsiteHttps");

            StringBuilder resultMailMessage =  mailMessage;

            resultMailMessage = resultMailMessage.Replace(_placeHolderEmailBody, mailBody.ToString());
            resultMailMessage = resultMailMessage.Replace(placeHolderHostRightsReserved, rightsReserved);
            resultMailMessage = resultMailMessage.Replace(placeHolderHostQuestionsOrSupportDetails, L("EHRAccountCreation_IfYouHaveAnyQuestionsOrNeedSupport", L("HostTechnicalSupportEmail")));
            resultMailMessage = resultMailMessage.Replace(placeHolderHostWebsite, hostWebsiteHttps);
            return resultMailMessage;
        }
    }
}