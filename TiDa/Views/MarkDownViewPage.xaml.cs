using TiDa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiDa.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TiDa.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarkDownViewPage : ContentPage
    {
        MarkDownDetailViewModel viewModel;
        public MarkDownViewPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MarkDownDetailViewModel();

        }


        private void Editor_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            
            MdView.Markdown = Editor.Text;
        }
    }
}