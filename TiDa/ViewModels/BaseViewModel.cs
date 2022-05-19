using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TiDa.Models;
using TiDa.Services;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        public IDataStore<CommonTask> CommonDataStore => DependencyService.Get<IDataStore<CommonTask>>();

        public IDataStore<WeekTask> WeekDataStore => DependencyService.Get<IDataStore<WeekTask>>();

        public IDataStore<MarkDownTask> MarkDownDataStore => DependencyService.Get<IDataStore<MarkDownTask>>();

        public IDataStore<SimpleWrPo> SimpleWrPoDataStore => DependencyService.Get<IDataStore<SimpleWrPo>>();

        public IDataStore<TomatoTask> TomatoDataStore => DependencyService.Get<IDataStore<TomatoTask>>();
        public IDataStore<TargetTask> TargetDataStore => DependencyService.Get<IDataStore<TargetTask>>();

        public IWebService<User> LoginWebService => DependencyService.Get<IWebService<User>>();
        public IWebService<UserInfo> RegisterWebService => DependencyService.Get<IWebService<UserInfo>>();

        public IDataWeb<CommonTask> CommonTaskWeb => DependencyService.Get<IDataWeb<CommonTask>>();

        public IDataWeb<MarkDownTask> MarkDownWeb => DependencyService.Get<IDataWeb<MarkDownTask>>();

        public IDataWeb<WeekTask> WeekTaskWeb => DependencyService.Get<IDataWeb<WeekTask>>();

        public IDataWeb<TomatoTask> TomoTaskWeb => DependencyService.Get<IDataWeb<TomatoTask>>();

        public IDataWeb<TargetTask> TargTaskTaskWeb => DependencyService.Get<IDataWeb<TargetTask>>();

        public IDataWeb<SimpleWrPo> SimpleWrPoWeb => DependencyService.Get<IDataWeb<SimpleWrPo>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
