using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiDa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterView : ContentPage
    {
        Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
        public RegisterView()
        {
            InitializeComponent();
        }


        private void Email_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Email.Text != null)
            {
                if (r.IsMatch(Email.Text))
                {
                    EmailValid.Text = "邮箱合法";
                    Register.IsEnabled = true;
                }
                else
                {
                    EmailValid.Text = "邮箱不合法";
                    Register.IsEnabled = false;
                }
            }
        }

        private void RePwd_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (RePwd.Text == null)
            {
                Register.IsEnabled = false;
            }
            else
            {
                if (Pwd.Text == RePwd.Text)
                {
                    Register.IsEnabled = true;
                    PwdValid.Text = "密码一致";
                }
                else
                {
                    Register.IsEnabled = false;
                    PwdValid.Text = "密码不一致";
                }
            }
        }
    }
}