using Microsoft.AspNetCore.Components;
using Plateaumed.EHR.Authorization.Accounts;
using Plateaumed.EHR.Authorization.Accounts.Dto;
using Plateaumed.EHR.Core.Dependency;
using Plateaumed.EHR.Core.Threading;
using Plateaumed.EHR.Mobile.MAUI.Models.Login;
using Plateaumed.EHR.Mobile.MAUI.Shared;

namespace Plateaumed.EHR.Mobile.MAUI.Pages.Login
{
    public partial class ForgotPasswordModal : ModalBase
    {
        public override string ModalId => "forgot-password-modal";
       
        [Parameter] public EventCallback OnSave { get; set; }
        
        public ForgotPasswordModel forgotPasswordModel { get; set; } = new ForgotPasswordModel();

        private readonly IAccountAppService _accountAppService;

        public ForgotPasswordModal()
        {
            _accountAppService = DependencyResolver.Resolve<IAccountAppService>();
        }

        protected virtual async Task Save()
        {
            await SetBusyAsync(async () =>
            {
                await WebRequestExecuter.Execute(
                async () =>
                    await _accountAppService.SendPasswordResetCode(new SendPasswordResetCodeInput { EmailAddress = forgotPasswordModel.EmailAddress }),
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
