﻿using System.Threading.Tasks;
using Plateaumed.EHR.Views;
using Xamarin.Forms;

namespace Plateaumed.EHR.Services.Modal
{
    public interface IModalService
    {
        Task ShowModalAsync(Page page);

        Task ShowModalAsync<TView>(object navigationParameter) where TView : IXamarinView;

        Task<Page> CloseModalAsync();
    }
}
