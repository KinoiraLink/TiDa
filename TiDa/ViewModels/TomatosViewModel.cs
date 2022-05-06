using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TiDa.Models;
using TiDa.Views;
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

        //******** 构造函数

        public TomatosViewModel()
        {
            TomatoTasks = new ObservableCollection<TomatoTask>();
            LoadTomatoTask = new Command(async () => await LoadFunc());
            AddTomatoCommand = new Command(AddFunc);
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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedTaskId = null;
        }
    }
}
