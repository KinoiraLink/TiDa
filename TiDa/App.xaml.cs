using System;
using TiDa.Services;
using TiDa.Views;
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
            DependencyService.Register<ICommonTaskStorage,CommonTaskStorage>();
            DependencyService.Register<IPreferenceStorage,PreferenceStoragecs>();


            MainPage = new AppShell();
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
