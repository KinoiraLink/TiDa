using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiDa.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiDa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TargetsView : ContentPage
    {
        TargetsViewModel _viewModel;
        public TargetsView()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TargetsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }

}