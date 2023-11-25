using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiDa.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.RichTextEditor;
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

            StackLayout stack = new StackLayout();

            SfRichTextEditor editor = new SfRichTextEditor
            {
                HeightRequest = 150,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Text = "The rich text editor component is WYSIWYG editor that provides the best user experience to create and update the content"
            };

            stack.Children.Add(editor);
            this.Content = stack;
        }


        //private async void MenuItem_Get(object sender, EventArgs e)
        //{
        //    var contentext = viewModel.GetContent();
        //    var s = await webView.EvaluateJavaScriptAsync($"contents('{contentext}')");
        //}


        //private async void MenuItem_Set(object sender, EventArgs e)
        //{
        //    var s = await webView.EvaluateJavaScriptAsync($"factorial()");
        //    viewModel.SetContent(s);
        //}
    }
}