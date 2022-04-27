using TiDa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TiDa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarkDownViewPage : ContentPage
    {
        public MarkDownViewPage()
        {
            InitializeComponent();

        }


        private void Editor_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            
            MdView.Markdown = Editor.Text;
        }
    }
}