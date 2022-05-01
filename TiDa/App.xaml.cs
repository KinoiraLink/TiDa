﻿using System;
using TiDa.Models;
using TiDa.Services;
using TiDa.ViewModels;
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
            DependencyService.Register<WeekTaskDataStorage>();
            DependencyService.Register<ICommonTaskStorage,CommonTaskStorage>();
            DependencyService.Register<IPreferenceStorage,PreferenceStoragecs>();
            DependencyService.Register<IDataStore<WeekTask>,WeekTaskDataStorage>();
            DependencyService.Register<IDataStore<MarkDownTask>, MarkDownTaskDataStorage>();
            
            DependencyService.Register<WeekTaskViewModel>();
            DependencyService.Register<MarkDownViewModel>();

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
