using Microsoft.AspNetCore.Components;
using Plateaumed.EHR.Authorization.Accounts;
using Plateaumed.EHR.Authorization.Accounts.Dto;
using Plateaumed.EHR.Core.Dependency;
using Plateaumed.EHR.Core.Threading;
using Plateaumed.EHR.Mobile.MAUI.Models.Login;
using Plateaumed.EHR.Mobile.MAUI.Shared;

namespace Plateaumed.EHR.Mobile.MAUI.Pages.Login
{
    public partial class EmailActivationModal : ModalBase
    {
        public override string ModalId => "email-activation-modal";

        [Parameter] public EventCallback OnSave { get; set; }

        public EmailActivationModel emailActivationModel { get; set; } = new EmailActivationModel();

        private readonly IAccountAppService _accountAppService;

        public EmailActivationModal()
        {
            _accountAppService = DependencyResolver.Resolve<IAccountAppService>();
        }

        protected virtual async Task Save()
        {
            await SetBusyAsync(async () =>
            {
                await WebRequestExecuter.Execute(
                async () =>
                    await _accountAppService.SendEmailActivationLink(new SendEmailActivationLinkInput
                    {
                        EmailAddress = emailActivationModel.EmailAddress
                    }),
                    async () =>
                    {
                        await OnSave.InvokeAsync();
                    }
                );
            });
        }

        protected virtual async Task Cancel()
        {
            await Hide();
        }
    }
}
