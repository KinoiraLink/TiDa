using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfSchedule.XForms;
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


        public Command CommonTapped { get; }
        public WeekTaskViewModel()
        {
            WeekTasks = new ObservableCollection<WeekTask>();

            LoadWeekTask = new Command(async () => await LoadWeekTaskFunction());

            CommonTapped = new Command(OnWeekTaskRefresh);
            TaskTapped = new Command<WeekTask>(OnWeekTaskSelected);
        }

        //private async void OnWeekTaskInsert()
        //{
        //    var weekTasks = new List<WeekTask>();
        //    if (!WeekDataStore.IsInitialized())
        //    {
        //        await WeekDataStore.InitializeAsync();
        //    }
        //    var itemsAsync = await WeekDataStore.GetAllItemsAsync();

        //    if (itemsAsync.Count()== 0)
        //    {
        //        for (int week = 1; week <= 7; week++)
        //        {
        //            for (int row = 1; row <= 8; row++)
        //            {
        //                weekTasks.Add(new WeekTask{Row = row,WeekDay = week,TaskTitle = "",TaskDescribe = "",Site = "",TaskTime = "",Timestamp = 0,UserCookie = 0});
        //            }
        //        }

        //        await WeekDataStore.InserAllItem(weekTasks);
        //    }
        //}

        private async void OnWeekTaskSelected(WeekTask week)
        {
            if(week !=null)
            {
                Preferences.Set("NavPara", week.Id);
                await PopupNavigation.Instance.PushAsync(new WeekTaskUpdateView());
            }
        }

        private IEnumerable<WeekTask> _weekTasks = new List<WeekTask>();
        private async Task LoadWeekTaskFunction()
        {
            IsBusy = true;

            try
            {   
                WeekTasks.Clear();
                if (!WeekDataStore.IsInitialized())
                {
                    await WeekDataStore.InitializeAsync();
                }
                _weekTasks = await WeekDataStore.GetItemsAsync(true);
                if (_weekTasks.Count() == 0)
                {
                    
                    await WeekDataStore.InserAllItem(WeekTasksInit);

                    await Application.Current.MainPage.DisplayAlert("提示", "第一次使用正在生成初始数据", "Ok");

                    LoadWeekTaskFunction();
                }

                foreach (var weekTask in _weekTasks)
                {
                    WeekTasks.Add(weekTask);
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


        async void OnWeekTaskRefresh()
        {
            if (Preferences.Get("token", "undefined").Equals("undefined"))
            {
                await Application.Current.MainPage.DisplayAlert("提示", "数据同步请先登录", "Ok");
            }
            else
            {
                //await CommonTaskWeb.UploadAsync(CommonTasks);
            }
            await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedWeekTask = null;
        }


        public List<WeekTask> WeekTasksInit = new List<WeekTask>
        {
            new WeekTask{Id = 1,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 2,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 3,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 4,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 5,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 6,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 7,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 8,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 9,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 10,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 11,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 12,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 13,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 14,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 15,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 16,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 17,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 18,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 19,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 20,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 21,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 22,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 23,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 24,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 25,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 26,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 27,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 28,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 29,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 30,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 31,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 32,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 33,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 34,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 35,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 36,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 37,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 38,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 39,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 40,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 41,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 42,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 43,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 44,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 45,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 46,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 47,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 48,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 49,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 50,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 51,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 52,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 53,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 54,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 55,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
            new WeekTask{Id = 56,TaskTitle = "语文",TaskTime = "08:01:00",TaskDescribe = "早读",Site = "宿舍",Timestamp = 0},
        };
    }
}
