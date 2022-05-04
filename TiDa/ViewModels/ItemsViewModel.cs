using System;
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

        private CommonTask _selectedTaskId;
        public CommonTask SelectedTaskId
        {
            get => _selectedTaskId;
            set
            {
                SetProperty(ref _selectedTaskId, value);
            }
        }


        public ObservableCollection<CommonTask> CommonTasks { get; }

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
            LoadCommonTask = new Command(async () => await LoadCommonTaskFunction());
            CommonTapped = new Command(OnCommonTaskSelected);

            DeleteCmmonTaskCommand = new Command<CommonTask>(DeleteCmmonTaskCommandFunction);
            AddorUpCommonTaskCommand = new Command<CommonTask>(AddorUpCommonTaskCommandFunction);
            DoneCommand = new Command<CommonTask>(DoneFunc);
        }

        private async void DoneFunc(CommonTask common)
        {
            IsBusy = true;

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
                LoadCommonTaskFunction();
            }
        }


        async void AddorUpCommonTaskCommandFunction(CommonTask common)
        {
            if (common!=null)
            {
                
                Preferences.Set("NavPara", common.Id);
            }
            else
            {
                Preferences.Set("NavPara", 0);
            }
            await PopupNavigation.Instance.PushAsync(new NewCommonTaskPopupPage());
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
                LoadCommonTaskFunction();
            }

        }

        public async Task LoadCommonTaskFunction()
        {
            IsBusy = true;

            try
            {
                if (!CommonDataStore.IsInitialized())
                {
                    await CommonDataStore.InitializeAsync();
                }

                CommonTasks.Clear();
                var commonTasks = await CommonDataStore.GetItemsAsync(true);
                foreach (var commonTask in commonTasks)
                {
                    CommonTasks.Add(commonTask);
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
    }
}