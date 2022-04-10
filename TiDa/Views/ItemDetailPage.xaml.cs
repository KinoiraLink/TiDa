using System.ComponentModel;
using TiDa.ViewModels;
using Xamarin.Forms;

namespace TiDa.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}