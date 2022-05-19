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

            AppActions.OnAppAction += AppActions_OnAppAction;
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTkzNDQxQDMyMzAyZTMxMmUzMGhaZkFMZ0ZXVzNrRVoxMXExaGptSFNEOUN2NzFxR3VxSGVSYmpQS01VV0E9");
            Preferences.Set("token", "undefined");
            ResourcesHelper.LoadTheme(Theme.Dark);
            MainPage = new AppShell();
           
        }

        private void AppActions_OnAppAction(object sender, AppActionEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (e.AppAction.Id == "simple_wrpo")
                    await Shell.Current.GoToAsync("//" + nameof(SimpleWrPoEidt));
                if (e.AppAction.Id == "tomato")
                    await Shell.Current.GoToAsync("//" + nameof(TomatosView));
                if (e.AppAction.Id == "common")
                    await Shell.Current.GoToAsync("//" + nameof(ItemsPage));
                if (e.AppAction.Id == "week")
                    await Shell.Current.GoToAsync("//" + nameof(WeekTaskPage));
                if (e.AppAction.Id == "markdown")
                    await Shell.Current.GoToAsync("//" + nameof(MarkDownTasksViewPage));
                if (e.AppAction.Id == "markdown")
                    await Shell.Current.GoToAsync("//" + nameof(TargetsView));

            });
        }

        protected override void OnStart()
        {
            try
            {
                AppActions.SetAsync(new AppAction("simple_wrpo", "随记随拍"), 
                    new AppAction("tomato", "番茄时间"),
                    new AppAction("common","一般清单"),
                    new AppAction("week","周期列表"),
                    new AppAction("markdown","记录清单"),
                    new AppAction("target","目标清单"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
