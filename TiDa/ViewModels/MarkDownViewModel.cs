using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using TiDa.Models;
using TiDa.Views;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public class  MarkDownViewModel : BaseViewModel
    {
        private MarkDownTask _selecteMarkDownTask;

        public MarkDownTask SelecteMarkDownTask
        {
            get => _selecteMarkDownTask;
            set
            {
                SetProperty(ref _selecteMarkDownTask, value);
                SaveFunction(value);
            }
        }

        public ObservableCollection<MarkDownTask> MarkDownTasks { get; }

        private MarkDownTask _markDownTask;

        public MarkDownTask MarkDownTask
        {
            get => _markDownTask;
            set => SetProperty(ref _markDownTask, value);
        }

        //******** 绑定命令
        public Command LoadMarkDownTaskCommand { get; }
        

        public Command AddMarkDownTaskCommand { get; }

        public Command<MarkDownTask> SaveMarkDownTaskCommand { get; }

        public MarkDownViewModel()
        {
            MarkDownTasks = new ObservableCollection<MarkDownTask>();
            LoadMarkDownTaskCommand = new Command(async () => await LoadFunction());
            AddMarkDownTaskCommand = new Command(AddFunction);
            SaveMarkDownTaskCommand = new Command<MarkDownTask>(SaveFunction);
        }

        

        async Task LoadFunction()
        {
            IsBusy = true;

            try
            {
                if (!MarkDownDataStore.IsInitialized())
                {
                    await MarkDownDataStore.InitializeAsync();
                }

                MarkDownTasks.Clear();

                var markDownTasks = await MarkDownDataStore.GetItemsAsync();
                foreach (var markDownTask in markDownTasks)
                {
                    MarkDownTask = new MarkDownTask();
                    MarkDownTask.Id = markDownTask.Id;
                    MarkDownTask.Timestamp = markDownTask.Timestamp;
                    MarkDownTask.TaskTitle = markDownTask.TaskTitle;
                    if (markDownTask.TaskContent.Length < 20|| markDownTask.TaskContent==null)
                    {
                        MarkDownTask.TaskContent = markDownTask.TaskContent;
                    }
                    else
                    {
                        
                        MarkDownTask.TaskContent=markDownTask.TaskContent.Substring(0,20);
                    }

                    
                    MarkDownTasks.Add(MarkDownTask);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void SaveFunction(MarkDownTask markDownTask)
        {

            if (markDownTask !=null)
            {
                await Shell.Current.GoToAsync($"//MarkPage?{nameof(MarkDownDetailViewModel.Id)}={markDownTask.Id}");
            }

            
        }

        async void AddFunction( )
        {
            await PopupNavigation.Instance.PushAsync(new MarkDownNewPage());
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelecteMarkDownTask = null;
        }
    }
}
