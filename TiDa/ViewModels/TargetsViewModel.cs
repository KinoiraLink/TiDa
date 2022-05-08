using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using TiDa.Models;
using TiDa.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public  class TargetsViewModel : BaseViewModel
    {
        private TargetTask _selectedTaskId;

        public TargetTask SelectedTaskId
        {
            get => _selectedTaskId;
            set
            {
                SetProperty(ref _selectedTaskId, value);
            } 
        }

        private int DoingCount = 0;

        private int DoneId = 0;

        public ObservableCollection<TargetList> TargetsLists { get; }

        public ObservableCollection<TargetTask> IsDoneTargetTasks { get; }

        public List<TargetTask> minorList { get; }

        public Command LoadTargetCommand { get; }

        public Command AddMinCommand { get; }

        public Command<TargetList> AddMinorCommand { get; }

        public Command<TargetTask> DeleteCommand { get; }

        public Command<TargetTask> DoneCommand { get; }

        public Command RefreshCommand { get; }


        public TargetsViewModel()
        {
            TargetsLists = new ObservableCollection<TargetList>();
            minorList = new List<TargetTask>();
            IsDoneTargetTasks = new ObservableCollection<TargetTask>();
            LoadTargetCommand = new Command(async () => await LoadFunc());
            AddMinCommand = new Command(AddMinFunc);
            AddMinorCommand = new Command<TargetList>(AddMinorFunc);
            DeleteCommand = new Command<TargetTask>(DeleteFunc);
            DoneCommand = new Command<TargetTask>(DoneFunc);
            RefreshCommand = new Command(RefreshFunc);
        }

       

        private async void DoneFunc(TargetTask target)
        {
            IsBusy = true;

            if (target != null)
            {
                try
                {
                    var targetTask = new TargetTask
                    {
                        Id = target.Id,
                        MainTitle = target.MainTitle,
                        MinorTitle = target.MinorTitle,
                        IsDelete = target.IsDelete,
                        IsDone = true,
                        Timestamp = DateTime.Now.Ticks
                    };
                    await TargetDataStore.InsertorReplace(targetTask);

                    var allItemsAsync = await TargetDataStore.GetAllItemsAsync();
                    //判断是否全部做完
                    await Task.Run(() =>
                    {
                        
                        foreach (var task in allItemsAsync)
                        {
                            if (task.MainTitle == target.MainTitle)
                            {
                                IsDoneTargetTasks.Add(task);
                            }

                        }

                        foreach (var isDoneTargetTask in IsDoneTargetTasks)
                        {
                            if (!isDoneTargetTask.IsDone && isDoneTargetTask.MainTitle != isDoneTargetTask.MinorTitle)
                            {
                                DoingCount += 1;
                            }

                            if (isDoneTargetTask.MainTitle == isDoneTargetTask.MinorTitle)
                            {
                                DoneId = isDoneTargetTask.Id;
                            }

                        }
                    });
                    

                    if (DoingCount ==0)
                    {
                        await TargetDataStore.InsertorReplace(new TargetTask
                        {
                            Id=DoneId,
                            MainTitle = target.MainTitle,
                            MinorTitle = target.MainTitle,
                            IsDelete = target.IsDelete,
                            IsDone = true,
                            Timestamp = DateTime.Now.Ticks
                        });
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;

                    IsDoneTargetTasks.Clear();
                    DoingCount = 0;
                    await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
                }
            }
        }

        private async void DeleteFunc(TargetTask target)
        {
            IsBusy = true;

            if (target != null)
            {
                try
                {
                    var targetTask = new TargetTask
                    {
                        Id = target.Id,
                        MainTitle = target.MainTitle,
                        MinorTitle = target.MinorTitle,
                        IsDelete = true,
                        IsDone = true,
                        Timestamp = DateTime.Now.Ticks
                    };
                    //var commonTask = new CommonTask
                    //{
                    //    Id = common.Id,
                    //    Done = common.Done,
                    //    IsDeleted = true,
                    //    TaskDate = common.TaskDate,
                    //    TaskDescribe = common.TaskDescribe,
                    //    TaskTime = common.TaskTime,
                    //    TaskTitle = common.TaskTitle,
                    //    Timestamp = DateTime.Now.Ticks,
                    //    UserCookie = common.UserCookie
                    //};

                    //CommonTasks.Remove(common);
                    await TargetDataStore.InsertorReplace(targetTask);
                    
                    
                    

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                    //CommonTasks.Remove(common);
                    await Shell.Current.GoToAsync($"{nameof(JumpPage)}");
                }
            }
        }

        private async void AddMinorFunc(TargetList targetTask)
        {
            if (targetTask != null)
            {
                Preferences.Set("NavParaStr",targetTask.MainTitle);
                await PopupNavigation.Instance.PushAsync(new TMinorNewView());
            }
        }

        private async void AddMinFunc()
        {
            await PopupNavigation.Instance.PushAsync(new TMainNewView());
        }

        private async Task LoadFunc()
        {
            IsBusy = true;
            try
            {
                if (!TargetDataStore.IsInitialized())
                {
                    await TargetDataStore.InitializeAsync();
                }

                TargetsLists.Clear();
                minorList.Clear();
                var targetTasks = await TargetDataStore.GetItemsAsync(true);
                foreach (var targetTask in targetTasks)
                {
                    if (targetTask.MainTitle == targetTask.MinorTitle)
                    {
                        var maintitel = targetTask.MainTitle;
                        foreach (var task in targetTasks)
                        {
                            if (task.MainTitle == maintitel && task.MinorTitle != task.MainTitle)
                            {
                                minorList.Add(task);
                            }
                        }

                        var targetList = new TargetList()
                        {
                            MainTitle = maintitel,
                            MinorList = new List<TargetTask>(minorList)

                        };
                        
                        TargetsLists.Add(targetList);
                        minorList.Clear();
                    }
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
