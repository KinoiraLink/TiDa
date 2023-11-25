using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using TiDa.Models;
using TiDa.Views;
using TiDa.ViewModels;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using TiDa.Services;
using Xamarin.Essentials;

namespace TiDa.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {

        INotificationManager notificationManager;

        private CommonTask _selectedTaskId;
        public CommonTask SelectedTaskId
        {
            get => _selectedTaskId;
            set
            {
                SetProperty(ref _selectedTaskId, value);
                DoneFunc(value);
            }
        }


        public ObservableCollection<CommonTask> CommonTasks { get; }

        public ObservableCollection<CommonTask> TodayTasks { get; }

        //******** 绑定命令
        public Command LoadCommonTask { get; }

        public Command DeleteCmmonTaskCommand { get; }

        public Command  AddorUpCommonTaskCommand { get; }


        public Command CommonTapped { get; }

        public Command DoneCommand { get; }


        public ItemsViewModel()
        {
            Title = "清单";
            CommonTasks = new ObservableCollection<CommonTask>();
            TodayTasks = new ObservableCollection<CommonTask>();
            LoadCommonTask = new Command(async () => await LoadCommonTaskFunction());
            CommonTapped = new Command(OnCommonTaskSelected);

            DeleteCmmonTaskCommand = new Command<CommonTask>(DeleteCmmonTaskCommandFunction);
            AddorUpCommonTaskCommand = new Command<CommonTask>(AddorUpCommonTaskCommandFunction);
            DoneCommand = new Command<CommonTask>(DoneFunc);

            if (Device.RuntimePlatform == Device.Android)
            {
                notificationManager = DependencyService.Get<INotificationManager>();
                notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                //ShowNotification(evtData.Title, evtData.Message);
            };
            
            Device.StartTimer(TimeSpan.FromSeconds(30.0), ShowMessage);
            }

            
        }

        public async void DoneFunc(CommonTask common)
        {
            IsBusy = true;

            if (common != null)
            {
                try
                {
                    var commonTask = new CommonTask
                    {
                        Id = common.Id,
                        Done = true,
                        IsDeleted = common.IsDeleted,
                        TaskDate = common.TaskDate,
                        TaskDescribe = common.TaskDescribe,
                        TaskTime = common.TaskTime,
                        TaskTitle = common.TaskTitle,
                        Timestamp = DateTime.Now.Ticks,
                        UserCookie = common.UserCookie
                    };

                    CommonTasks.Remove(common);
                    await CommonDataStore.InsertorReplace(commonTask);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;

                    CommonTasks.Remove(common);
                    await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
                }
            }
        }

        //跳转一般清单详情页
        async void AddorUpCommonTaskCommandFunction(CommonTask common)
        {
            //非空，则为点击事务记录本身，跳转事务详情页
            if (common!=null)
            {
                //由于使用PopupPage，PopupPage本身不支持参数导航，使用偏好存储传参，可能影响程序
                Preferences.Set("NavPara", common.Id);
            }
            else
            {
                //SQLite中数据编号从1开始
                Preferences.Set("NavPara", 0);
            }
            await PopupNavigation.Instance.PushAsync(new CommonTaskNewPopupPage());
        }
        
        async void DeleteCmmonTaskCommandFunction(CommonTask common)
        {

            IsBusy = true;

            try
            {
                var commonTask = new CommonTask
                {
                    Id = common.Id,
                    Done = common.Done,
                    IsDeleted = true,
                    TaskDate = common.TaskDate,
                    TaskDescribe = common.TaskDescribe,
                    TaskTime = common.TaskTime,
                    TaskTitle = common.TaskTitle,
                    Timestamp = DateTime.Now.Ticks,
                    UserCookie = common.UserCookie
                };

                CommonTasks.Remove(common);
                await CommonDataStore.InsertorReplace(commonTask);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                CommonTasks.Remove(common);
                await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
            }

        }

        //加载数据
        public async Task LoadCommonTaskFunction()
        {
            IsBusy = true;
            string Today=DateTime.Now.Year.ToString() + "." + 
                         DateTime.Now.Month.ToString() + "." + 
                         DateTime.Now.Day.ToString() + " " + 
                         DateTime.Now.DayOfWeek.ToString();
            try
            {
                //数据库是否初始化，如果没有就初始化,就初始化
                if (!CommonDataStore.IsInitialized())
                {
                    await CommonDataStore.InitializeAsync();
                }

                CommonTasks.Clear();
                var commonTasks = await CommonDataStore.GetItemsAsync(true);

                foreach (var commonTask in commonTasks)
                {
                    CommonTasks.Add(commonTask);
                    if (commonTask.TaskDate.Equals(Today))
                    {
                        TodayTasks.Add(commonTask);
                    }
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
            SelectedTaskId = null;
        }

 


        async void OnCommonTaskSelected()
        {
            if (Preferences.Get("token", "undefined").Equals("undefined"))
            {
                await Application.Current.MainPage.DisplayAlert("提示", "数据同步请先登录", "Ok");
            }
            else
            {
                await CommonTaskWeb.UploadAsync(CommonTasks);
            }
            await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
        }


        public bool ShowMessage()
        {
            string title;
            string message;
            if (TodayTasks.Count != 0)
            {
                foreach (var commonTask in TodayTasks)
                {
                    var time = commonTask.TaskTime.Split(':');
                    int hours = Int32.Parse(time[0]);
                    if (DateTime.Now.Hour+1 == hours)
                    {
                        title = commonTask.TaskTitle; 
                        message = commonTask.TaskDescribe+"\n将在1小时后开始";
                        notificationManager.SendNotification(title, message);
                        return true;
                    }
                }
            }

            return false;



        }


    }
}