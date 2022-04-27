using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using TiDa.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiDa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeekTaskUpdateView : PopupPage
    {
        WeekTaskUpdateViewModel viewModel;
        public WeekTaskUpdateView()
        {
            InitializeComponent();
            viewModel = new WeekTaskUpdateViewModel();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.OnDisappearing();
        }
    }
}