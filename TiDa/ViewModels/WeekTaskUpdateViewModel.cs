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
    public class WeekTaskUpdateViewModel : BaseViewModel
    {

        private WeekTask _weekTask;

        public WeekTask WeekTask
        {
            get => _weekTask;
            set => SetProperty(ref _weekTask, value);
        }
        private string _taskTitle;

        public string TaskTitle
        {
            get => _taskTitle;
            set => SetProperty(ref _taskTitle, value);
        }

        private string _taskDescribe;

        public string TaskDescribe
        {
            get => _taskDescribe;
            set => SetProperty(ref _taskDescribe, value);
        }

        private TimeSpan _tasktime;

        public TimeSpan TaskTime
        {
            get => _tasktime;
            set => SetProperty(ref _tasktime, value);
        }

        private string _site;

        public string Site
        {
            get => _site;
            set => SetProperty(ref _site, value);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(TaskTitle);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public WeekTaskUpdateViewModel()
        {
            CancelCommand = new Command(() => PopupNavigation.Instance.PopAsync(true));

            SaveCommand = new Command(OnSave, ValidateSave);
            Loaded();
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private async void Loaded()
        {
            if (Preferences.Get("NavPara", 0) != 0)
            {
                var Id = Preferences.Get("NavPara", 0);
                WeekTask = await WeekDataStore.GetItemAsync(Id.ToString());
                TaskTitle = WeekTask.TaskTitle;
                TaskDescribe = WeekTask.TaskDescribe;
                var time = WeekTask.TaskTime.Split(':');
                TaskTime = new TimeSpan(int.Parse(time[0]), int.Parse(time[1]), 0);
                Site = WeekTask.Site;
            }
        }

        private async void OnSave()
        {
            WeekTask.TaskTitle = TaskTitle;
            WeekTask.TaskDescribe = TaskDescribe;
            WeekTask.TaskTime = TaskTime.ToString();
            WeekTask.Site= Site;
            WeekTask.Timestamp = DateTime.Now.Ticks;
            await WeekDataStore.InsertorReplace(WeekTask);
            await PopupNavigation.Instance.PopAsync(true);
        }


        public async void OnDisappearing()
        {
            await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
        }
    }
}
