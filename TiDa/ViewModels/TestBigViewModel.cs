using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace TiDa.ViewModels
{

    internal class TestBigViewModel : BaseViewModel
    {
        private TestLitteViewModel _testLitteViewModel;
        public ObservableCollection<TestLitteViewModel> BigCollection { get; } =
            new ObservableCollection<TestLitteViewModel>();

        public Command NewCommand { get; }

        

        public TestBigViewModel()
        {
            NewCommand = new Command(AddFunction);

        }

        public void AddFunction( )
        {
            BigCollection.Add(new TestLitteViewModel());
        }


    }
}

