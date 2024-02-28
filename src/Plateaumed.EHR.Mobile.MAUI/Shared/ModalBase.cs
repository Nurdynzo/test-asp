using Plateaumed.EHR.Core.Dependency;
using Plateaumed.EHR.Mobile.MAUI.Services.UI;

namespace Plateaumed.EHR.Mobile.MAUI.Shared
{
    public abstract class ModalBase : EHRComponentBase
    {
        protected ModalManagerService ModalManager { get; set; }

        public abstract string ModalId { get; }

        public ModalBase()
        {
            ModalManager = DependencyResolver.Resolve<ModalManagerService>();
        }

        public virtual async Task Show()
        {
            await ModalManager.Show(JS, ModalId);
            StateHasChanged();
        }

        public virtual async Task Hide()
        {
            await ModalManager.Hide(JS, ModalId);
        }
    }
}
