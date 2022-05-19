using System;
using TiDa.Helpers;
using TiDa.Models;
using TiDa.Services;
using TiDa.ViewModels;
using TiDa.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiDa
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<CommonTaskDataStorage>();
            DependencyService.Register<WeekTaskDataStorage>();
            DependencyService.Register<ICommonTaskStorage,CommonTaskStorage>();
            DependencyService.Register<IPreferenceStorage,PreferenceStoragecs>();
            DependencyService.Register<IDataStore<WeekTask>,WeekTaskDataStorage>();
            DependencyService.Register<IDataStore<MarkDownTask>, MarkDownTaskDataStorage>();
            DependencyService.Register<IDataStore<SimpleWrPo>, SimpleWrPoDataStorage>();
            DependencyService.Register<IDataStore<TomatoTask>,TomatoDataStorage>();
            DependencyService.Register<IDataStore<TargetTask>, TargetDataStorage>();

            DependencyService.Register<IWebService<User>,LoginWebService>();
            DependencyService.Register<IWebService<UserInfo>, RegisterInfoWebService>();
            DependencyService.Register<IDataWeb<CommonTask>, CommonWeb>();
            DependencyService.Register<IDataWeb<MarkDownTask>, MarkDownWeb>();
            DependencyService.Register<IDataWeb<WeekTask>, WeekWeb>();
            DependencyService.Register<IDataWeb<TomatoTask>, TomatoWeb>();
            DependencyService.Register<IDataWeb<TargetTask>, TargetWeb>();
            DependencyService.Register<IDataWeb<SimpleWrPo>, SimpleWrPoWeb>();



            DependencyService.Register<WeekTaskViewModel>();
            DependencyService.Register<MarkDownViewModel>();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTkzNDQxQDMyMzAyZTMxMmUzMGhaZkFMZ0ZXVzNrRVoxMXExaGptSFNEOUN2NzFxR3VxSGVSYmpQS01VV0E9");
            Preferences.Set("token", "undefined");
            MainPage = new AppShell();
            ResourcesHelper.LoadTheme(Theme.Dark);
        }

        protected override void OnStart()
        {
             
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
