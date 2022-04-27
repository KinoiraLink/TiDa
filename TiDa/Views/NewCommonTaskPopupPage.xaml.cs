using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using TiDa.Services;
using TiDa.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiDa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCommonTaskPopupPage : PopupPage
    {
        CommonTaskNewItemViewModel _taskNewItemViewModel;
        public NewCommonTaskPopupPage()
        {
            InitializeComponent();
            var i = Preferences.Get("NavPara", 0);
            Preferences.Clear("NavPara");
            _taskNewItemViewModel = new CommonTaskNewItemViewModel();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _taskNewItemViewModel.OnDisappearing();
        }

    }
}