using System;
using System.Collections.Generic;
using System.Text;
using Rg.Plugins.Popup.Services;
using TiDa.Models;
using TiDa.Views;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public class MarkDownNewViewModel : MarkDownViewModel
    {
        private string _taskTitle;

        public string TaskTitle
        {
            get => _taskTitle;
            set => SetProperty(ref _taskTitle, value);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(TaskTitle);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public MarkDownNewViewModel()
        {
            SaveCommand = new Command(SaveFuntion, ValidateSave);
            CancelCommand = new Command(CancelFuntion);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        async void CancelFuntion()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        public async void SaveFuntion()
        {
            var markDownTask = new MarkDownTask
            {
                TaskTitle = TaskTitle,
                Timestamp = DateTime.Now.Ticks,
                TaskContent = ""

            };
            await MarkDownDataStore.InserItemAsync(markDownTask);
            await PopupNavigation.Instance.PopAsync(true);
        }

        public async void OnDisappearing()
        {
            await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
        }

    }
}
