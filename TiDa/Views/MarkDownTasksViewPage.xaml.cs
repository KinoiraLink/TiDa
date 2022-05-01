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
    public partial class MarkDownTasksViewPage : ContentPage
    {
        MarkDownViewModel viewModel;
        public MarkDownTasksViewPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MarkDownViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}