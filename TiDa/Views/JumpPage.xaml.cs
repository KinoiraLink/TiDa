using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiDa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JumpPage : ContentPage
    {
        public JumpPage()
        {
            InitializeComponent();
            //Jumpto();
        }

        public async void Jumpto()
        {
            await Shell.Current.GoToAsync("..",false);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Jumpto();
            //await Shell.Current.GoToAsync("..",false);
        }

        private async  void Button_OnClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}