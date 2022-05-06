using System;
using System.Collections.Generic;
using System.Text;
using Rg.Plugins.Popup.Services;
using TiDa.Models;
using TiDa.Views;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public class TMainNewViewModel : BaseViewModel
    {
        private string _mainTitle;

        public string MainTitle
        {
            get => _mainTitle;
            set => SetProperty(ref _mainTitle, value);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(MainTitle);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public TMainNewViewModel()
        {
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
            var targetTask = new TargetTask
            {
                MainTitle = MainTitle,
                MinorTitle = MainTitle,
                IsDelete = false,
                IsDone = false,
                Timestamp = DateTime.Now.Ticks
            };

            await TargetDataStore.InserItemAsync(targetTask);   

            await PopupNavigation.Instance.PopAsync(true);
        }


        public async void OnDisappearing()
        {
            await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
        }
    }
}
