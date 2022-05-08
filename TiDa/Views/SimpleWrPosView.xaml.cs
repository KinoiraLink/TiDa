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
    public partial class SimpleWrPosView : ContentPage
    {
        SimpelWrPosViewModel simpelWrPosViewModel;
        public SimpleWrPosView()
        {
            InitializeComponent();
            BindingContext = simpelWrPosViewModel = new SimpelWrPosViewModel();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            simpelWrPosViewModel.OnAppearing();
        }
    }
}