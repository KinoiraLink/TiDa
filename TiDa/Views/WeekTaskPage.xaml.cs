using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TiDa.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiDa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeekTaskPage : ContentPage
    {
        private WeekTaskViewModel _weekTaskViewModel;
        private double scaleY;
        public WeekTaskPage()
        {
            InitializeComponent();

            BindingContext = _weekTaskViewModel = new WeekTaskViewModel();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _weekTaskViewModel.OnAppearing();
            
        }


    }
}