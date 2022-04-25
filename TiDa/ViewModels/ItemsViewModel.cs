using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TiDa.Models;
using TiDa.Views;
using TiDa.ViewModels;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        private CommonTask _selectedTaskId;

        public CommonTask SelectedTaskId
        {
            get => _selectedTaskId;
            set
            {
                SetProperty(ref _selectedTaskId, value);
                OnCommonTaskSelected(value);
                DeleteCmmonTaskCommandFunction(value);
            }
        }
        
        public ObservableCollection<Item> Items { get; }

        public ObservableCollection<CommonTask> CommonTasks { get; }

        //******** 绑定命令
        public Command LoadCommonTask { get; }
        public Command LoadItemsCommand { get; }

        public Command DeleteCmmonTaskCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public Command<CommonTask> CommonTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            CommonTasks = new ObservableCollection<CommonTask>();
            LoadCommonTask = new Command(async () => await LoadCommonTaskFunction());
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            CommonTapped = new Command<CommonTask>(OnCommonTaskSelected);
            //ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            DeleteCmmonTaskCommand = new Command<CommonTask>(DeleteCmmonTaskCommandFunction);
        }

        async void DeleteCmmonTaskCommandFunction(CommonTask common)
        {
            IsBusy = true;

            try
            {
                await CommonDataStore.DeleteItemAsync(common);
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

        async Task LoadCommonTaskFunction()
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

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
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
            SelectedItem = null;
            SelectedTaskId = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        async void OnCommonTaskSelected(CommonTask commonTask)
        {
            if(commonTask == null)return;
            await Shell.Current.GoToAsync(
                $"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={commonTask.Id}");
        }
    }
}