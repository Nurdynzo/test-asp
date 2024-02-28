using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Uow;
using Plateaumed.EHR.Chat;
using Plateaumed.EHR.Facilities;
using Plateaumed.EHR.Net.Emailing.ContactSales;

namespace Plateaumed.EHR.Authorization.Users
{
    public interface IUserEmailer
    {
        /// <summary>
        /// Send email activation link to user's email address.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="link">Email activation link</param>
        /// <param name="plainPassword">
        /// Can be set to user's plain password to include it in the email.
        /// </param>
        Task SendEmailActivationLinkAsync(User user, string link, string plainPassword = null);

        /// <summary>
        /// Sends a password reset link to user's email.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="link">Password reset link (optional)</param>
        Task SendPasswordResetLinkAsync(User user, string link = null);

        /// <summary>
        /// Sends email activation link to facility user.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="link">Activation link</param>
        /// <returns></returns>
        Task SendEmailActivationLinkToFacilityUserAsync(User user, string targetEditionName, string link = null);

        /// <summary>
        /// Sends an email for unread chat message to user's email.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="senderUsername"></param>
        /// <param name="senderTenancyName"></param>
        /// <param name="chatMessage"></param>
        Task TryToSendChatMessageMail(User user, string senderUsername, string senderTenancyName, ChatMessage chatMessage);

        /// <summary>
        /// Sends sales team RFQ (Request for Quote) email.
        /// </summary>
        /// <param name="facility">Facility</param>
        /// <param name="request">EHR Feature requests from host customers</param>
        /// <param name="userFullName">Fullname of the requester of RFQ</param>
        Task SendRFQToSalesTeam(Facility facility, QuotationRequest request, string userFullName);

        /// <summary>
        /// Sends customers RFQ (Request for Quote) email.
        /// </summary>
        /// <param name="facility">User</param>
        /// <param name="request">EHR Feature requests from host customers</param>
        Task SendRFQDetailsToUsers(Facility facility, QuotationRequest request, string userFullName);

        /// <summary>
        /// Send Login Email Credentials To Staff
        /// </summary>
        /// <param name="user"></param>
        /// <param name="link"></param>
        /// <param name="plainPassword"></param>
        /// <returns></returns>
        Task SendEmailActivationLinkToStaffAsync(User user, string link, string plainPassword = null);

        /// <summary>
        /// Send email to receiver on shared registration
        /// </summary>
        /// <param name="user"></param>
        /// <param name="senderName"></param>
        /// <param name="facilityName"></param>
        /// <param name="tenantName"></param>
        /// <returns></returns>
        Task SendEmailToReceiverOnSharedRegistrationAsync(string emailAddress, string senderName, string tenancyName, int tenantId, string link);

        /// <summary>
        /// Send email to sender on shared registration
        /// </summary>
        /// <param name="user"></param>
        /// <param name="recipients"></param>
        /// <param name="facilityName"></param>
        /// <returns></returns>
        Task SendEmailToSenderOnSharedRegistrationAsync(User user, string recipients, string facilityName);

        /// <summary>
        /// Send patient consent document
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="link"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task SendEmailConsentDocumentStatementAsync(List<string> recipients, string link, int? tenantId);
    }
}
