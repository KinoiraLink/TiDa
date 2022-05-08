using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TiDa.Models;
using Xamarin.Forms;

namespace TiDa.ViewModels
{
    public class SimpelWrPosViewModel : BaseViewModel
    {
        private SimpleWrPo _selectedId;

        public SimpleWrPo SelectedId
        {
            get => _selectedId;
            set => SetProperty(ref _selectedId, value);
        }


        public ObservableCollection<SimpleWrPo> SimpleWrPos { get; }

        public Command LoadSimpleCommand { get; }

        public Command<SimpleWrPo> SimpleTapped { get; }

        public SimpelWrPosViewModel()
        {
            SimpleWrPos = new ObservableCollection<SimpleWrPo>();
            LoadSimpleCommand = new Command(async () => await LoadFunc());
            SimpleTapped = new Command<SimpleWrPo>(SelectFunc);
        }

        private void SelectFunc(SimpleWrPo simpleWrPo)
        {
            if (simpleWrPo == null)
                return;
            //Todo


        }

        private async Task LoadFunc()
        {
            IsBusy = true;

            try
            {
                SimpleWrPos.Clear();
                var simpleWrPos = await SimpleWrPoDataStore.GetItemsAsync(true);
                foreach (var simpleWrPo in simpleWrPos)
                {
                    SimpleWrPos.Add(simpleWrPo);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedId = null;
        }
    }
}
