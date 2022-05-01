using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiDa.Models;
using TiDa.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private User _loginUser;

        public User LoginUser
        {
            get => _loginUser;
            set => SetProperty(ref _loginUser, value);
        }

        private string _acount;

        public string Account
        {
            get => _acount;
            set => SetProperty(ref _acount, value);
        }

        private string _pwd;

        public string Pwd
        {
            get => _pwd;
            set => SetProperty(ref _pwd, value);
        }

        private bool _isLoginIn;

        public bool IsLoginIn
        {
            get => _isLoginIn;
            set => SetProperty(ref _isLoginIn, value);
        }

        private bool _isLoginOut;

        public bool IsLoginOut
        {
            get => _isLoginOut;
            set => SetProperty(ref _isLoginOut, value);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_acount)
                   && !String.IsNullOrWhiteSpace(_pwd);
        }


        public Command LoginCommand { get; }
        public Command GoLoginCommand { get; }

        public Command GoOutCommand { get; }

        public Command ReutrnCommand { get; }

        public LoginPageViewModel()
        {
            LoginCommand = new Command(LoginFunc, ValidateSave);
            GoLoginCommand = new Command(GoLoginFunction);
            GoOutCommand = new Command(GoOutFunc);
            ReutrnCommand =new Command(ReturnFunc);
            this.PropertyChanged +=
                (_, __) => LoginCommand.ChangeCanExecute();


            if (Preferences.Get("token", 0) == 0)
            {
                IsLoginIn = true;
                IsLoginOut = false;
            }
            else
            {
                IsLoginIn = false;
                IsLoginOut = true;
            }
        }

        private async void ReturnFunc()
        {
            Preferences.Set("token", 0);
            IsLoginIn = true;
            IsLoginOut = false;
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }

        private async void GoOutFunc()
        {
            Preferences.Set("token", 0);
            IsLoginIn = true;
            IsLoginOut = false;
            await Shell.Current.GoToAsync($"..");
        }

        private async void GoLoginFunction()
        {
            IsLoginIn = false;
            IsLoginOut = true;
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }


        private async void LoginFunc()
        {
            var user = new User();
            user.account = Account;
            user.pwd = Pwd;
            LoginUser = await LoginWebService.LgoinAsync(user);
            if (LoginUser.msg.Equals("登录成功"))
            {
                
                await Application.Current.MainPage.DisplayAlert("提示", LoginUser.msg, "OK");
                Preferences.Set("token", LoginUser.token);
                IsLoginIn = false;
                IsLoginOut = true;
                await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
            }

            if (LoginUser.msg.Equals("密码错误"))
            {
                await Application.Current.MainPage.DisplayAlert("提示", LoginUser.msg, "OK");
            }


        }
    }
}
