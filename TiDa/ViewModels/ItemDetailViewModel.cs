using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TiDa.Models;
using TiDa.ViewModels;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                //LoadItemId(value);
                LoadTaskId(value);
            }
        }


        private string _taskTitle;

        public string TaskTitle
        {
            get => _taskTitle;
            set => SetProperty(ref _taskTitle, value);
        }

        private string _taskDescribe;

        public string TaskDescribe
        {
            get => _taskDescribe;
            set => SetProperty( ref _taskDescribe, value);
        }
        private string text;

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        private string description;

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
              //  Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        public async void LoadTaskId(string itemId)
        {
            try
            {
                var commonTask = await CommonDataStore.GetItemAsync(itemId);
                TaskTitle = commonTask.TaskTitle;
                TaskDescribe = commonTask.TaskDescribe;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
