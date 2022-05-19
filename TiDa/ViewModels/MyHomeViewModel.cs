using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiDa.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public class MyHomeViewModel : BaseViewModel
    {
        private string _account;

        public string Account
        {
            get => _account;
            set => SetProperty(ref _account, value);
        }
        private UserInfo _info;

        public UserInfo Info
        {
            get => _info;
            set => SetProperty(ref _info, value);
        }

        private int _lCommonCount;

        public int LCommonCount
        {
            get => _lCommonCount;
            set => SetProperty(ref _lCommonCount, value);
        }

        private int _rCommonCount;

        public int RCommonCount
        {
            get => _rCommonCount;
            set => SetProperty(ref _rCommonCount, value);
        }

        private int _lWeekCount;

        public int LWeekCount
        {
            get => _lWeekCount;
            set => SetProperty(ref _lWeekCount, value);
        }

        private int _rWeekCount;

        public int RWeekCount
        {
            get => _rWeekCount;
            set => SetProperty(ref _rWeekCount, value);
        }

        private int _lMarkCount;

        public int LMarkCount
        {
            get => _lMarkCount;
            set => SetProperty(ref _lMarkCount, value);
        }

        private int _rMarkCount;

        public int RMarkCount
        {
            get => _rMarkCount;
            set => SetProperty(ref _rMarkCount, value);
        }

        private int _lTargetCount;

        public int LTargetCount
        {
            get => _lTargetCount;
            set => SetProperty(ref _lTargetCount, value);
        }

        private int _rTargetCount;

        public int RTargetCount
        {
            get => _rTargetCount;
            set => SetProperty(ref _rTargetCount, value);
        }

        private int _lTomatoCount;

        public int LTomatoCount
        {
            get => _lTomatoCount;
            set => SetProperty(ref _lTomatoCount, value);
        }

        private int _rTomatoCount;

        public int RTomatoCount
        {
            get => _rTomatoCount;
            set => SetProperty(ref _rTomatoCount, value);
        }

        private int _lSimpleCount;

        public int LSimpleCount
        {
            get => _lSimpleCount;
            set => SetProperty(ref _lSimpleCount, value);
        }

        private int _rSimpleCount;

        public int RSimpleCount
        {
            get => _rSimpleCount;
            set => SetProperty(ref _rSimpleCount, value);
        }

        public Command Refresh { get; }

        public MyHomeViewModel()
        {
            LoadFunc();
        }

        public async Task LoadFunc()
        {
            try
            {
                if (CommonDataStore.IsInitialized())
                {
                    await CommonDataStore.InitializeAsync();
                }

                if (WeekDataStore.IsInitialized())
                {
                    await WeekDataStore.InitializeAsync();
                }

                if (MarkDownDataStore.IsInitialized())
                {
                    await MarkDownDataStore.InitializeAsync();
                }

                if (TargetDataStore.IsInitialized())
                {
                    await TargetDataStore.InitializeAsync();
                }

                if (TomatoDataStore.IsInitialized())
                {
                    await TomatoDataStore.InitializeAsync();
                }

                if (SimpleWrPoDataStore.IsInitialized())
                {
                    await SimpleWrPoDataStore.InitializeAsync();
                }

                var token = Preferences.Get("token", "undefined");
                Account = Preferences.Get("account", "undefined");
                var userInfo = await RegisterWebService.LgoinAsync(new UserInfo
                {
                    UserCookie = token
                });

                if (userInfo != null)
                {
                    Info = userInfo;

                }

                LCommonCount = await CommonDataStore.GetItemsCount();
                RCommonCount = await CommonTaskWeb.CountAsync(new CommonTask
                {
                    UserCookie = token
                });
                LWeekCount = await WeekDataStore.GetItemsCount();
                LMarkCount = await MarkDownDataStore.GetItemsCount();
                RMarkCount = await MarkDownWeb.CountAsync(new MarkDownTask
                {
                    UserCookie = token
                });
                LTargetCount = await TargetDataStore.GetItemsCount();
                RTargetCount = await TargTaskTaskWeb.CountAsync(new TargetTask
                {
                    UserCookie = token
                });
                LTomatoCount = await TomatoDataStore.GetItemsCount();
                RTomatoCount = await TomoTaskWeb.CountAsync(new TomatoTask
                {
                    UserCookie = token
                });
                LSimpleCount = await SimpleWrPoDataStore.GetItemsCount();
                RSimpleCount = await SimpleWrPoWeb.CountAsync(new SimpleWrPo
                {
                    UserCookie = token
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async void OnAppearing()
        {
            if (Preferences.Get("token", "undefined").Equals("undefined"))
            {
                await Application.Current.MainPage.DisplayAlert("提示", "请先登录", "Ok");
                await Shell.Current.GoToAsync("//index/task/ItemsPage");
            }
        }
    }
}
