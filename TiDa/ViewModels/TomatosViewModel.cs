using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TiDa.Models;
using TiDa.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public class TomatosViewModel : BaseViewModel
    {
        //******** 绑定属性
        private TomatoTask _selectedTaskId;

        public TomatoTask SelectedTaskId
        {
            get => _selectedTaskId;
            set
            {
                SetProperty(ref _selectedTaskId, value);
            }
            
        }

        public ObservableCollection<TomatoTask> TomatoTasks { get; set; }

        //******** 绑定命令

        public Command LoadTomatoTask { get; }

        public Command AddTomatoCommand { get; }

        public Command RefreshCommand { get; }

        //******** 构造函数

        public TomatosViewModel()
        {
            TomatoTasks = new ObservableCollection<TomatoTask>();
            LoadTomatoTask = new Command(async () => await LoadFunc());
            AddTomatoCommand = new Command(AddFunc);
            RefreshCommand = new Command(RefreshFunc);
        }

        

        private async void AddFunc()
        {
            await Shell.Current.GoToAsync(nameof(TomatoNewView));
        }

        private async Task LoadFunc()
        {
            IsBusy = true;
            try
            {
                if (!TomatoDataStore.IsInitialized())
                {
                    await TomatoDataStore.InitializeAsync();
                }

                TomatoTasks.Clear();
                var tomatoTasks = await TomatoDataStore.GetItemsAsync(true);
                foreach (var tomatoTask in tomatoTasks)
                {
                    TomatoTasks.Add(tomatoTask);
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

        private async void RefreshFunc()
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
            SelectedTaskId = null;
        }
    }
}
