using System;
using System.Collections.Generic;
using System.Text;
using TiDa.Models;
using TiDa.Views;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public class TomatoNewViewModel : BaseViewModel
    {

        //******** 私有变量
        private bool isAdd = false;
        private int oneSecond = 0;
        private int oneMin = 0;
        private int oneHour = 0;

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(TaskTitle);
        }

        //******** 绑定属性

        //计时时间
        private string _second = "0";

        public string ASecond
        {
            get => _second;
            set => SetProperty(ref _second, value);
        }

        private string _minute ="0";

        public string AMinute
        {
            get => _minute;
            set => SetProperty(ref _minute, value);
        }

        private string _hour = "0";

        public string AHour
        {
            get => _hour;
            set => SetProperty(ref _hour, value);
        }



        //现在时间
        private double _hourHandRotation;

        public double HourHandRotation
        {
            get => _hourHandRotation;
            set => SetProperty(ref _hourHandRotation, value);
        }

        private double _minuteHandRotation;

        public double MinuteHandRotation
        {
            get => _minuteHandRotation;
            set => SetProperty(ref _minuteHandRotation, value);
        }

        private double _secondHandRotation;

        public double SecondHandRotation
        {
            get => _secondHandRotation;
            set => SetProperty(ref _secondHandRotation, value);
        }

        //清单相关
        private string _taskTitle;

        public string TaskTitle
        {
            get => _taskTitle;
            set => SetProperty(ref _taskTitle, value);
        }

        private string _taskDescribe;

        public string TaskDescribe
        {
            get => _taskDescribe;
            set => SetProperty(ref _taskDescribe, value);
        }

        

        public Command CancelCommand { get; set; }

        public Command SaveCommand { get; set; }
        public Command TimeKeeper { get; }

        public TomatoNewViewModel()
        {
            CancelCommand = new Command(CancelFunc);
            TimeKeeper = new Command(KeeperFunc);
            SaveCommand = new Command(SaveFunc,ValidateSave);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            Device.StartTimer(TimeSpan.FromSeconds(1.0), OnTimerTick);
        }

        private async void SaveFunc()
        {
            TomatoTask tomatoTask = new TomatoTask()
            {
                TaskTitle = TaskTitle,
                TaskDescribe = TaskDescribe,
                TaskTime = AHour+":"+AMinute+":"+ASecond,
                Timestamp = DateTime.Now.Ticks
            };
            await TomatoDataStore.InserItemAsync(tomatoTask);
            isAdd = false;
            await Shell.Current.GoToAsync("..");

        }

        private async void CancelFunc()
        {
            isAdd = false;
            await Shell.Current.GoToAsync("..");
        }
        //计时器功能函数
        private void KeeperFunc()
        {
            isAdd = !isAdd;
            //StartTimer需要一个返回类型为布尔类型且返回值为trued的方法触发，第一个参数为触发时间间隔
            Device.StartTimer(TimeSpan.FromSeconds(1.0), OnTimer);
        }
        //计时器的计时时间
        bool OnTimer()
        {
            oneSecond += 1;
            if (oneSecond == 60)
            {
                oneMin += 1;
                oneSecond = 0;
                if (oneMin == 0)
                {
                    oneHour += 1;
                    oneMin = 0;
                }
            }
            ASecond = oneSecond.ToString();
            AMinute = oneMin.ToString();
            AHour = oneHour.ToString();
            return isAdd;
        }
        //钟表的计时
        bool OnTimerTick()
        {
            DateTime dateTime = DateTime.Now;
            HourHandRotation = 30 * (dateTime.Hour % 12) + 0.5 * dateTime.Minute;
            MinuteHandRotation = 6 * dateTime.Minute + 0.1 * dateTime.Second;
            double t = dateTime.Millisecond / 1000.0;
            if (t < 0.5)
            {
                t = 0.5 * Easing.SpringIn.Ease(t / 0.5);
            }
            else
            {
                t = 0.5 * (1 + Easing.SpringOut.Ease((t - 0.5) / 0.5));
            }
            SecondHandRotation = 6 * (dateTime.Second + t);
            return true;
        }
    }
}
