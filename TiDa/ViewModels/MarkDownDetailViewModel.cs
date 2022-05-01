using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TiDa.Models;
using TiDa.Views;
using Xamarin.Forms;

namespace TiDa.ViewModels
{

    [QueryProperty(nameof(Id), nameof(Id))]
    public class MarkDownDetailViewModel : BaseViewModel
    {
        private int _id;

        public int Id
        {
            get => _id;
            set {
                SetProperty(ref _id, value);
                LoadItemId(value.ToString());
            }
        }

        private MarkDownTask _markDownTask;

        public MarkDownTask MarkDownTask
        {
            get => _markDownTask;
            set => SetProperty(ref _markDownTask, value);
        }

        private string _tasktitle;

        public string TaskTitle
        {
            get => _tasktitle;
            set
            {
                _tasktitle =value;
                
            }
        }

        private string _taskConten;

        public string TaskContent
        {
            get => _taskConten;
            set => SetProperty(ref _taskConten, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public MarkDownDetailViewModel()
        {
            SaveCommand = new Command(SaveFunction);
            CancelCommand = new Command(CancelFuntion);
        }

        async void CancelFuntion()
        {
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }

        async void SaveFunction()
        {
            try
            {
                var markDownTask = await MarkDownDataStore.GetItemAsync(Id.ToString());
                markDownTask.TaskContent=TaskContent;
                markDownTask.TaskTitle=TaskTitle;
                markDownTask.Timestamp = DateTime.Now.Ticks;
                await MarkDownDataStore.InsertorReplace(markDownTask);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
            finally
            {
                await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
            }

            
        }

        public async void LoadItemId(string Id)
        {
            try
            {
                var markDownTask = await MarkDownDataStore.GetItemAsync(Id);
                
                TaskContent = markDownTask.TaskContent;
                TaskTitle = markDownTask.TaskTitle;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
