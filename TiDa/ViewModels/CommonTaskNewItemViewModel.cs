using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Rg.Plugins.Popup.Services;
using TiDa.Models;
using TiDa.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public class CommonTaskNewItemViewModel : BaseViewModel
    {
        //todo 可能修改
        CommonTask commonTask = new CommonTask();

        private CommonTask common = new CommonTask();

        private String _taskTitle;

        public String TaskTitle
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

        private DateTime _taskDate;

        public DateTime TaskDate
        {
            get => _taskDate;
            set => SetProperty(ref _taskDate, value);
        }

        private TimeSpan _taskTime;

        public TimeSpan TaskTime
        {
            get => _taskTime;
            set => SetProperty(ref _taskTime, value);
        }

        private CommonTask _selectedCommonTask;

        public CommonTask SelectedCommonTask
        {
            get => _selectedCommonTask;
            set => SetProperty(ref _selectedCommonTask, value);
        }

        public Command DeleteCmmonTaskCommand { get; }


        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(TaskTitle);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        

        public CommonTaskNewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);

            DeleteCmmonTaskCommand = new Command<CommonTask>(DeleteCmmonTaskCommandFunction);
            AddorUp();
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();

        }

        private async void AddorUp()
        {
            var Id = Preferences.Get("NavPara", 0);
            if (Id == 0)
            {
                TaskDate = DateTime.Now;
                
            }
            else
            { 
                common = await CommonDataStore.GetItemAsync(Id.ToString());
                TaskTitle = common.TaskTitle;
                TaskDescribe = common.TaskDescribe;
                var dateAndTime = common.TaskDate.Split(' ');
                var date = dateAndTime[0].Split('.');
                TaskDate = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                var time = common.TaskTime.Split(':');
                TaskTime= new TimeSpan(int.Parse(time[0]), int.Parse(time[1]), 0);
            }

            
        }

        async void DeleteCmmonTaskCommandFunction(CommonTask commonTask)
        {
            IsBusy = true;

            try
            {
                await CommonDataStore.DeleteItemAsync(commonTask);
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

        private void OnCancel()
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        private async void OnSave()
        {
            if (Preferences.Get("NavPara", 0) != 0)
            {
                commonTask = common;
            }

            
            commonTask.TaskTitle = TaskTitle;
            commonTask.TaskDescribe = TaskDescribe;
            commonTask.TaskDate = TaskDate.Year.ToString()+"." 
                +TaskDate.Month.ToString()+"." 
                +TaskDate.Day.ToString()+ " "
                +TaskDate.DayOfWeek.ToString();
            commonTask.TaskTime = TaskTime.ToString();
            commonTask.Timestamp = DateTime.Now.Ticks;
            if (Preferences.Get("NavPara", 0) != 0)
            {
                await CommonDataStore.InsertorReplace(commonTask);
            }
            else
            {
                await CommonDataStore.InserItemAsync(commonTask);
            }

            await  PopupNavigation.Instance.PopAsync(true);
            
        }

        public async void OnDisappearing()
        {
            await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
        }

    }
}
