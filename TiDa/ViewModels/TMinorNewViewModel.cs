using System;
using System.Collections.Generic;
using System.Text;
using Rg.Plugins.Popup.Services;
using TiDa.Models;
using TiDa.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public class TMinorNewViewModel : BaseViewModel
    {
        private string _minorTitle;

        public string MinorTitle
        {
            get => _minorTitle;
            set => SetProperty(ref _minorTitle, value);
        }

        private string _mainTitle;

        public string MainTitle
        {
            get => _mainTitle;
            set => SetProperty(ref _mainTitle, value);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(MinorTitle);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public TMinorNewViewModel()
        {
            MainTitle=Preferences.Get("NavParaStr", "undefined");
            SaveCommand = new Command(SaveFuntion, ValidateSave);
            CancelCommand = new Command(CancelFuntion);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private async void CancelFuntion()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void SaveFuntion()
        {
            if (!MainTitle.Equals("undefined"))
            {
                var targetTask = new TargetTask
                {
                    MainTitle = MainTitle,
                    MinorTitle = MinorTitle,
                    IsDelete = false,
                    IsDone = false,
                    Timestamp = DateTime.Now.Ticks
                };
                await TargetDataStore.InserItemAsync(targetTask);
            }

            
            await PopupNavigation.Instance.PopAsync(true);
        }


        public async void OnDisappearing()
        {
            await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
        }
    }
}
