using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TiDa.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public  class MyAccountViewModel : BaseViewModel
    {
        private string _account;

        public string Account
        {
            get => _account;
            set => SetProperty(ref _account, value);
        }
        private UserInfo _info;

        public UserInfo Info
        {
            get => _info;
            set => SetProperty(ref _info, value);
        }

        public Command LoadCommand { get; }

        public MyAccountViewModel()
        {
            Info = new UserInfo();
            LoadCommand = new Command(async () => await LoadCommonTaskFunction());
            LoadCommonTaskFunction();
        }

        private async Task LoadCommonTaskFunction()
        {
            IsBusy = true;

            try
            {
                var token = Preferences.Get("token", "undefined");
                Account = Preferences.Get("account", "undefined");
                var userInfo = await RegisterWebService.LgoinAsync(new UserInfo
                {
                    UserCookie = token 
                });

                if (userInfo!=null)
                {
                    Info = userInfo;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


        public async void OnAppearing()
        {
            
            if (Preferences.Get("token", "undefined").Equals("undefined"))
            {
                await Application.Current.MainPage.DisplayAlert("提示", "请先登录", "Ok");
                await Shell.Current.GoToAsync("//index/task/ItemsPage");
            }
            else
            {
                Info = null;
                IsBusy = true;
            }
        }
    }
}
