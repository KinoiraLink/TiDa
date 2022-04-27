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
        private Item _selectedItem;

        

        PreferenceStoragecs _preferenceStorage;

        private CommonTask _selectedTaskId;
        public CommonTask SelectedTaskId
        {
            get => _selectedTaskId;
            set
            {
                SetProperty(ref _selectedTaskId, value);
                //OnCommonTaskSelected(value);
                DeleteCmmonTaskCommandFunction(value);

            }
        }
        
        public ObservableCollection<Item> Items { get; }

        public ObservableCollection<CommonTask> CommonTasks { get; }

        //******** 绑定命令
        public Command LoadCommonTask { get; }
        public Command LoadItemsCommand { get; }

        public Command DeleteCmmonTaskCommand { get; }

        public Command  AddorUpCommonTaskCommand { get; }

        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public Command CommonTapped { get; }



        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            CommonTasks = new ObservableCollection<CommonTask>();
            LoadCommonTask = new Command(async () => await LoadCommonTaskFunction());
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            CommonTapped = new Command(OnCommonTaskSelected);
            //ItemTapped = new Command<Item>(OnItemSelected);

           // AddItemCommand = new Command<CommonTask>(OnAddItem);

            DeleteCmmonTaskCommand = new Command<CommonTask>(DeleteCmmonTaskCommandFunction);
            AddorUpCommonTaskCommand = new Command<CommonTask>(AddorUpCommonTaskCommandFunction);
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

        private async void OnAddItem(Command obj)
        {
            //await Shell.Current.GoToAsync(nameof(NewItemPage));
            
            Preferences.Set("NavPara",12);
            await PopupNavigation.Instance.PushAsync(new NewCommonTaskPopupPage());



        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        async void OnCommonTaskSelected()
        {
            await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
        }
    }
}