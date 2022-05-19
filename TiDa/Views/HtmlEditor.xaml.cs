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
    public partial class HtmlEditor : ContentPage
    {
        HtmlEditorViewModel viewModel;
        public HtmlEditor()
        {
            InitializeComponent();
            BindingContext = viewModel = new HtmlEditorViewModel();
        }


        private async void MenuItem_Get(object sender, EventArgs e)
        {
            var contentext = viewModel.GetContent();
            var s = await webView.EvaluateJavaScriptAsync($"contents('{contentext}')");
        }


        private async void MenuItem_Set(object sender, EventArgs e)
        {
            var s = await webView.EvaluateJavaScriptAsync($"factorial()");
            viewModel.SetContent(s);
        }
    }
}