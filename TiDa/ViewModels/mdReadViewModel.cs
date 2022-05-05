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
    public  class mdReadViewModel : BaseViewModel
    {

        private int _id;

        public int Id
        {
            get => _id;
            set
            {
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
                _tasktitle = value;

            }
        }

        public string _taskConten;

        public string TaskContent
        {
            get => _taskConten;
            set => SetProperty(ref _taskConten, value);
        }

        public Command CancelCommand { get; }

        public mdReadViewModel()
        {
            CancelCommand = new Command(CancelFuntion);
        }
        async void CancelFuntion()
        {
            await Shell.Current.GoToAsync($"//{nameof(BoosView)}");
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
