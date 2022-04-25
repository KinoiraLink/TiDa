using System.ComponentModel;
using TiDa.ViewModels;
using Xamarin.Forms;

namespace TiDa.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        private ItemDetailViewModel _ItemDetailViewModel;
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = _ItemDetailViewModel = new ItemDetailViewModel(); ;
        }
    }
}