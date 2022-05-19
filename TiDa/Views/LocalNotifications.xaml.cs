using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiDa.Models;
using TiDa.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiDa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalNotifications : ContentPage
    {
        INotificationManager notificationManager;
        int notificationNumber = 0;
        public LocalNotifications()
        {
            InitializeComponent();
            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                //ShowNotification(evtData.Title, evtData.Message);
            };

        }

        private void OnSendClick(object sender, EventArgs e)
        {
            notificationNumber++;
            //string title = $"Local Notification #{notificationNumber}";
            //string message = $"You have now received {notificationNumber} notifications!";
            string title = "运动会";
            string message = "跑步项目第七组\n将在1小时后开始";
            notificationManager.SendNotification(title, message);
        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                stackLayout.Children.Add(msg);
            });
        }
    }
}