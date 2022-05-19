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
                //是否第一次进入该页
                if (_weekTasks.Count() == 0)
                {
                    //初始化数据
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
                await WeekTaskWeb.UploadAsync(WeekTasks);
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
            new WeekTask{Id = 1,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 2,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 3,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 4,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 5,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 6,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 7,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 8,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 9,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 10,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 11,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 12,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 13,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 14,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 15,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 16,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 17,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 18,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 19,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 20,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 21,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 22,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 23,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 24,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 25,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 26,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 27,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 28,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 29,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 30,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 31,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 32,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 33,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 34,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 35,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 36,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 37,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 38,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 39,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 40,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 41,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 42,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 43,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 44,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 45,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 46,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 47,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 48,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 49,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 50,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 51,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 52,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 53,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 54,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 55,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
            new WeekTask{Id = 56,TaskTitle = "",TaskTime = "12:00:00",TaskDescribe = "",Site = "",Timestamp = 0},
        };
    }
}
