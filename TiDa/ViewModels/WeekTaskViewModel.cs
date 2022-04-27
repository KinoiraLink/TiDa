using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using TiDa.Models;
using TiDa.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    internal class WeekTaskViewModel : BaseViewModel
    {

        private WeekTask _selectedWeekTask;

        public WeekTask SelectedWeekTask
        {
            get => _selectedWeekTask;
            set
            {
                SetProperty(ref _selectedWeekTask, value);
                OnWeekTaskSelected(value);
            }

        }

        public ObservableCollection<WeekTask> WeekTasks { get; }
        public ObservableCollection<WeekTask> MonWeekTasks { get; }
        public ObservableCollection<WeekTask> TueWeekTasks { get; }

        public ObservableCollection<WeekTask> WedWeekTasks { get; }

        public ObservableCollection<WeekTask> ThurWeekTasks { get; }

        public ObservableCollection<WeekTask> FriWeekTasks { get; }

        public ObservableCollection<WeekTask> SatWeekTasks { get; }

        public ObservableCollection<WeekTask> SunWeekTasks { get; }


        //******** 绑定命令
        public Command LoadWeekTask { get; }

        public Command<WeekTask> TaskTapped { get; }
        public WeekTaskViewModel()
        {
            WeekTasks = new ObservableCollection<WeekTask>();
            MonWeekTasks = new ObservableCollection<WeekTask>();
            TueWeekTasks = new ObservableCollection<WeekTask>();
            WedWeekTasks = new ObservableCollection<WeekTask>();
            ThurWeekTasks = new ObservableCollection<WeekTask>();
            FriWeekTasks = new ObservableCollection<WeekTask>();
            SatWeekTasks = new ObservableCollection<WeekTask>();
            SunWeekTasks = new ObservableCollection<WeekTask>();
            LoadWeekTask = new Command(async () => await LoadWeekTaskFunction());
            TaskTapped = new Command<WeekTask>(OnWeekTaskSelected);
        }

        private async void OnWeekTaskSelected(WeekTask week)
        {
            if(week !=null)
            {
                Preferences.Set("NavPara", week.Id);
                await PopupNavigation.Instance.PushAsync(new WeekTaskUpdateView());
            }
        }

        private async Task LoadWeekTaskFunction()
        {
            IsBusy = true;

            try
            {
                if (!WeekDataStore.IsInitialized())
                {
                    await WeekDataStore.InitializeAsync();
                }

                MonWeekTasks.Clear();
                TueWeekTasks.Clear();
                WedWeekTasks.Clear();
                ThurWeekTasks.Clear();
                FriWeekTasks.Clear();
                SatWeekTasks.Clear();
                SunWeekTasks.Clear();
                var weekTasks = await WeekDataStore.GetItemsAsync(true);
                foreach (var weekTask in weekTasks)
                {
                    if(weekTask.WeekDay==1) MonWeekTasks.Add(weekTask);
                    if(weekTask.WeekDay==2) TueWeekTasks.Add(weekTask);
                    if(weekTask.WeekDay==3) WedWeekTasks.Add(weekTask);
                    if(weekTask.WeekDay==4) ThurWeekTasks.Add(weekTask);
                    if(weekTask.WeekDay==5) FriWeekTasks.Add(weekTask);
                    if(weekTask.WeekDay==6) SatWeekTasks.Add(weekTask);
                    if(weekTask.WeekDay==7) SunWeekTasks.Add(weekTask);
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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedWeekTask = null;
        }
    }
}
