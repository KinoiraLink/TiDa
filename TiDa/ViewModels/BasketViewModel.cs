using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TiDa.Models;
using TiDa.Services;
using TiDa.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace TiDa.ViewModels
{
    public class BasketViewModel : BaseViewModel
    {
        private CommonTask _selectedCommon;

        public CommonTask SelectCommon
        {
            get => _selectedCommon;
            set => SetProperty(ref _selectedCommon, value);
        }

        public ObservableCollection<CommonTask> CommonTasks { get; }
        public ObservableCollection<MarkDownTask> MarkDownTasks { get; }
        public ObservableCollection<TargetTask> TargetTasks { get; }


        public MvvmHelpers.Commands.Command<CommonTask> DeComCommand { get; }
        public MvvmHelpers.Commands.Command<CommonTask> BackCommand { get; }
        public Command<MarkDownTask> DeMarCommand { get; }
        public Command<TargetTask> DeTarCommand { get; }

        public Command<TargetTask> BackTarCommand { get; }




        public Command LoadCommand { get; }

        public BasketViewModel()
        {
            CommonTasks = new ObservableCollection<CommonTask>();
            MarkDownTasks = new ObservableCollection<MarkDownTask>();
            TargetTasks = new ObservableCollection<TargetTask>();
            LoadCommand = new Command(async () => await LoadFunc());
            DeComCommand = new MvvmHelpers.Commands.Command<CommonTask>(DeleteCommonFunc);
            BackCommand = new MvvmHelpers.Commands.Command<CommonTask>(BackFunc);
            DeMarCommand = new Command<MarkDownTask>(DeleteMarkDownFunc);
            DeTarCommand = new Command<TargetTask>(DeleteTargetFunc);
            BackTarCommand = new Command<TargetTask>(BackTargetFunc);

        }

        private async void BackTargetFunc(TargetTask target)
        {
            await TargetDataStore.InsertorReplace(new TargetTask
            {
                Id = target.Id,
                IsDelete = false,
                IsDone = false,
                MainTitle = target.MainTitle,
                MinorTitle = target.MinorTitle,
                Timestamp = DateTime.Now.Ticks,
                UserCookie = Preferences.Get("token", "undefined")
            });

            await Shell.Current.GoToAsync($"{nameof(JumpPage)}");


        }

        private async void DeleteTargetFunc(TargetTask target)
        {
            await TargetDataStore.RomoveItemAsync(target);
            await TargTaskTaskWeb.DeleteAsync(target);
            await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
        }

        private async void DeleteMarkDownFunc(MarkDownTask markDown)
        {
            await MarkDownDataStore.RomoveItemAsync(markDown);
            await MarkDownWeb.DeleteAsync(markDown);
            await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
        }

        private async void BackFunc(CommonTask commonTask)
        {
            await CommonDataStore.InsertorReplace(new CommonTask
            {
                Id = commonTask.Id,
                Done = commonTask.Done,
                IsDeleted = false,
                TaskDate = commonTask.TaskDate,
                TaskDescribe = commonTask.TaskDescribe,
                TaskTime = commonTask.TaskTime,
                TaskTitle = commonTask.TaskTitle,
                Timestamp = DateTime.Now.Ticks,
                UserCookie = Preferences.Get("token", "undefined")
            });
            await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
        }

        private async void DeleteCommonFunc(CommonTask commonTask)
        {
            await CommonDataStore.RomoveItemAsync(commonTask);
            await CommonTaskWeb.DeleteAsync(commonTask);
            await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
        }

        private async Task LoadFunc()
        {
            IsBusy = true;

            try
            {
                if (!CommonDataStore.IsInitialized())
                {
                    await CommonDataStore.InitializeAsync();
                }

                if (!MarkDownDataStore.IsInitialized())
                {
                    await MarkDownDataStore.InitializeAsync();
                }

                if (!TargetDataStore.IsInitialized())
                {
                    await TargetDataStore.InitializeAsync();
                }

                CommonTasks.Clear();
                var commonTasks = await CommonDataStore.GetAllItemsAsync();

                MarkDownTasks.Clear();
                var markDownTasks = await MarkDownDataStore.GetAllItemsAsync();

                TargetTasks.Clear();
                var targetTasks = await TargetDataStore.GetAllItemsAsync();

                foreach (var commonTask in commonTasks)
                {
                    CommonTasks.Add(commonTask);
                }

                foreach (var markDownTask in markDownTasks)
                {
                    MarkDownTasks.Add(markDownTask);
                }

                foreach (var targetTask in targetTasks)
                {
                    TargetTasks.Add(targetTask);
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
        
        public async void OnAppearing()
        {
            if (Preferences.Get("token", "undefined").Equals("undefined"))
            {
                await Application.Current.MainPage.DisplayAlert("提示", "数据同步请先登录", "Ok");
                await Shell.Current.GoToAsync("//index/task/ItemsPage");
            }
            else
            {
                IsBusy = true;
            }
            
        }
    }
}
