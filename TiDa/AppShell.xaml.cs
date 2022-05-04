using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiDa.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TiDa
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(WeekTaskPage),typeof(WeekTaskPage));
            //Test
            //Todo be to Delete
            Routing.RegisterRoute(nameof(CommonTaskNewPopupPage), typeof(CommonTaskNewPopupPage));
           // Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
           Routing.RegisterRoute(nameof(JumpPage),typeof(JumpPage));
           Routing.RegisterRoute(nameof(Loading), typeof(Loading));
            Routing.RegisterRoute(nameof(MarkDownViewPage), typeof(MarkDownViewPage));
           Routing.RegisterRoute(nameof(MarkDownTasksViewPage), typeof(MarkDownTasksViewPage));

        }
    }
}
